# TP Web Services — Consommer un WSDL côté ASP.NET

## Énoncé du TP

**Sujet** : Créer un projet **ASP.NET Core** qui consomme un **WSDL** (service SOAP) et expose des **API REST** permettant de communiquer avec le système **legacy** (le TP SOAP Java fait précédemment).

**Entités exposées** : **TYPE** et **Produit** (chacune avec son DTO et son CRUD complet).

**Objectifs pédagogiques** :
- Comprendre le rôle d'un WSDL comme contrat neutre entre langages
- Savoir créer un serveur SOAP (Java/CXF) et générer son WSDL
- Savoir consommer un WSDL depuis un autre langage (ASP.NET Core)
- Mettre en place le pattern **REST façade over SOAP legacy**

---

## Architecture

```
Client mobile/web
    ↓  HTTP + JSON (REST)
┌──────────────────────────────┐
│   SenRestApi (ASP.NET Core)  │  ← le TP à rendre
│   port 5000                  │
│   - Controllers REST         │
│   - SoapClients (ChannelFactory)
└──────────────┬───────────────┘
               ↓  HTTP + SOAP/XML
┌──────────────────────────────┐
│ soap-server-java (Spring CXF)│  ← TP legacy
│ port 9090                    │
│ - /services/produits?wsdl    │
│ - /services/types?wsdl       │
└──────────────┬───────────────┘
               ↓ JDBC
        ┌─────────────┐
        │ In-Memory   │
        │ (démo)      │
        └─────────────┘
```

Le client REST ne sait pas que du SOAP se cache derrière : c'est le pattern "REST façade over SOAP" utilisé en entreprise pour moderniser l'accès à un système legacy sans le réécrire.

---

## Partie 1 — Serveur SOAP Java (`soap-server-java/`)

Spring Boot 3.2 + Apache CXF 4.0 — génère le WSDL automatiquement.

### Lancer

```bash
cd soap-server-java
mvn clean package -DskipTests
java -jar target/soap-server-1.0.0.jar
```

### WSDL disponibles

| Service | URL |
|---------|-----|
| Produits | `http://localhost:9090/services/produits?wsdl` |
| Types | `http://localhost:9090/services/types?wsdl` |

### Opérations SOAP exposées

**TypeService** (`Type` : id, libelle, description)
- `getAllTypes()`
- `getType(id)`
- `createType(type)`
- `updateType(id, type)`
- `deleteType(id)`

**ProduitService** (`Produit` : id, nom, prix, quantite, typeId)
- `getAllProduits()`
- `getProduit(id)`
- `createProduit(produit)`
- `updateProduit(id, produit)`
- `deleteProduit(id)`

### Structure

```
src/main/java/com/exemple/soap/
├── SoapServerApplication.java
├── config/CxfConfig.java          # publie /services/types et /services/produits
├── model/                         # Type.java, Produit.java
├── repository/                    # TypeRepository.java, ProduitRepository.java (in-memory)
└── service/                       # interfaces SEI + implémentations SOAP
```

---

## Partie 2 — API REST ASP.NET Core (`SenRestApi/`)

Consomme les WSDL Java via `ChannelFactory<T>` et expose des API REST.

### Lancer

```bash
cd SenRestApi
dotnet run --urls "http://0.0.0.0:5000"
```

(Le serveur SOAP Java doit tourner sur le port 9090.)

### Endpoints REST

| Méthode | Route | Description |
|---------|-------|-------------|
| GET | `/api/v1/produits` | Liste tous les produits |
| GET | `/api/v1/produits/{id}` | Produit par ID |
| POST | `/api/v1/produits` | Créer un produit |
| PUT | `/api/v1/produits/{id}` | Modifier un produit |
| DELETE | `/api/v1/produits/{id}` | Supprimer un produit |
| GET | `/api/v1/types` | Liste tous les types |
| GET | `/api/v1/types/{id}` | Type par ID |
| POST | `/api/v1/types` | Créer un type |
| PUT | `/api/v1/types/{id}` | Modifier un type |
| DELETE | `/api/v1/types/{id}` | Supprimer un type |

### Structure

```
SenRestApi/
├── Program.cs
├── Models/                    # Produit.cs, TypeProduit.cs
├── SoapClients/
│   ├── IProduitService.cs     # contrat SOAP (mêmes opérations que le WSDL)
│   ├── ITypeService.cs
│   └── SoapClientFactory.cs   # BasicHttpBinding + ChannelFactory
└── Controllers/
    ├── ProduitsController.cs  # REST → SOAP bridge
    └── TypesController.cs
```

### Exemples de test

```bash
# Liste des produits (passe par SOAP → Java)
curl http://localhost:5000/api/v1/produits

# Créer un produit
curl -X POST http://localhost:5000/api/v1/produits \
  -H "Content-Type: application/json" \
  -d '{"nom":"Écran 24\"","prix":150.0,"quantite":20,"typeId":1}'

# Liste des types
curl http://localhost:5000/api/v1/types
```

---

## Points clés pour l'oral

1. **WSDL = contrat neutre** : n'importe quel langage capable de le lire peut consommer le service (démontré avec les clients Java, PHP/Laravel et maintenant C#)
2. **CXF** = framework Java qui publie le service SOAP et génère le WSDL depuis les annotations `@WebService` / `@WebMethod`
3. **ASP.NET consomme le WSDL** via `ChannelFactory<T>` + `BasicHttpBinding` (équivalent du `wsdl2java` côté Java ou `ext-soap` côté PHP)
4. **REST façade over SOAP** : le client mobile/web consomme du JSON propre, le SOAP reste caché derrière
5. Les erreurs SOAP sont converties en réponses JSON propres (502) côté REST
