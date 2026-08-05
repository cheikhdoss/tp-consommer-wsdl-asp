package com.exemple.soap.repository;

import com.exemple.soap.model.Type;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicLong;

@Repository
public class TypeRepository {
    private final Map<Long, Type> store = new ConcurrentHashMap<>();
    private final AtomicLong counter = new AtomicLong();

    public TypeRepository() {
        save(new Type(null, "Informatique", "Matériel et accessoires informatiques"));
        save(new Type(null, "Bureau", "Fournitures de bureau"));
        save(new Type(null, "Maison", "Articles pour la maison"));
        save(new Type(null, "Sport", "Équipement sportif et fitness"));
        save(new Type(null, "Mode", "Vêtements et accessoires de mode"));
        save(new Type(null, "Alimentation", "Produits alimentaires et boissons"));
    }

    public List<Type> findAll() {
        return new ArrayList<>(store.values());
    }

    public Type findById(Long id) {
        return store.get(id);
    }

    public Type save(Type type) {
        if (type.getId() == null) {
            type.setId(counter.incrementAndGet());
        }
        store.put(type.getId(), type);
        return type;
    }

    public Type update(Long id, Type data) {
        Type existing = findById(id);
        if (existing == null) return null;
        if (data.getLibelle() != null) existing.setLibelle(data.getLibelle());
        if (data.getDescription() != null) existing.setDescription(data.getDescription());
        store.put(id, existing);
        return existing;
    }

    public boolean deleteById(Long id) {
        return store.remove(id) != null;
    }
}
