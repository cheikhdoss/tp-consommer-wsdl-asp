package com.exemple.soap.service;

import com.exemple.soap.model.Type;
import com.exemple.soap.repository.TypeRepository;
import jakarta.jws.WebService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.List;

@WebService(endpointInterface = "com.exemple.soap.service.TypeServiceSEI")
@Component
public class TypeServiceImpl implements TypeServiceSEI {

    @Autowired
    private TypeRepository repository;

    @Override
    public List<Type> getAllTypes() {
        return repository.findAll();
    }

    @Override
    public Type getType(Long id) {
        Type type = repository.findById(id);
        if (type == null) throw new RuntimeException("Type non trouvé : " + id);
        return type;
    }

    @Override
    public Type createType(Type type) {
        return repository.save(type);
    }

    @Override
    public Type updateType(Long id, Type type) {
        Type updated = repository.update(id, type);
        if (updated == null) throw new RuntimeException("Type non trouvé : " + id);
        return updated;
    }

    @Override
    public boolean deleteType(Long id) {
        return repository.deleteById(id);
    }
}
