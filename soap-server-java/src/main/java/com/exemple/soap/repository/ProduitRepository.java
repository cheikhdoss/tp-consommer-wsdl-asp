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
