package com.exemple.soap.service;

import com.exemple.soap.model.Produit;
import com.exemple.soap.repository.ProduitRepository;
import jakarta.jws.WebService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.List;

@WebService(endpointInterface = "com.exemple.soap.service.ProduitServiceSEI")
@Component
public class ProduitServiceImpl implements ProduitServiceSEI {

    @Autowired
    private ProduitRepository repository;

    @Override
    public List<Produit> getAllProduits() {
        return repository.findAll();
    }

    @Override
    public Produit getProduit(Long id) {
        Produit produit = repository.findById(id);
        if (produit == null) throw new RuntimeException("Produit non trouvé : " + id);
        return produit;
    }

    @Override
    public Produit createProduit(Produit produit) {
        return repository.save(produit);
    }

    @Override
    public Produit updateProduit(Long id, Produit produit) {
        Produit updated = repository.update(id, produit);
        if (updated == null) throw new RuntimeException("Produit non trouvé : " + id);
        return updated;
    }

    @Override
    public boolean deleteProduit(Long id) {
        return repository.deleteById(id);
    }
}
