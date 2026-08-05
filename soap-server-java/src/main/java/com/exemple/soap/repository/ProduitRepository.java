package com.exemple.soap.repository;

import com.exemple.soap.model.Produit;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicLong;

@Repository
public class ProduitRepository {
    private final Map<Long, Produit> store = new ConcurrentHashMap<>();
    private final AtomicLong counter = new AtomicLong();

    public ProduitRepository() {
        save(new Produit(null, "Laptop HP", 999.99, 10, 1L));
        save(new Produit(null, "Souris Logitech", 29.99, 50, 1L));
        save(new Produit(null, "Chaise ergonomique", 199.99, 5, 2L));
        save(new Produit(null, "Écran Dell 27 pouces", 299.99, 15, 1L));
        save(new Produit(null, "Clavier mécanique", 89.99, 30, 1L));
        save(new Produit(null, "Ramette papier A4", 4.99, 200, 2L));
        save(new Produit(null, "Aspirateur robot", 249.99, 8, 3L));
        save(new Produit(null, "Cafetière expresso", 129.99, 12, 3L));
        save(new Produit(null, "Ballon de foot", 24.99, 40, 4L));
        save(new Produit(null, "Haltères 10kg", 45.99, 20, 4L));
        save(new Produit(null, "T-shirt coton", 15.99, 100, 5L));
        save(new Produit(null, "Baskets running", 79.99, 25, 5L));
    }

    public List<Produit> findAll() {
        return new ArrayList<>(store.values());
    }

    public Produit findById(Long id) {
        return store.get(id);
    }

    public Produit save(Produit produit) {
        if (produit.getId() == null) {
            produit.setId(counter.incrementAndGet());
        }
        store.put(produit.getId(), produit);
        return produit;
    }

    public Produit update(Long id, Produit data) {
        Produit existing = findById(id);
        if (existing == null) return null;
        if (data.getNom() != null) existing.setNom(data.getNom());
        if (data.getPrix() != null) existing.setPrix(data.getPrix());
        if (data.getQuantite() != null) existing.setQuantite(data.getQuantite());
        if (data.getTypeId() != null) existing.setTypeId(data.getTypeId());
        store.put(id, existing);
        return existing;
    }

    public boolean deleteById(Long id) {
        return store.remove(id) != null;
    }
}
