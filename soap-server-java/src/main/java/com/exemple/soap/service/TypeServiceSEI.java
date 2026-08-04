package com.exemple.soap.service;

import com.exemple.soap.model.Type;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import jakarta.jws.WebService;

import java.util.List;

@WebService
public interface TypeServiceSEI {

    @WebMethod
    List<Type> getAllTypes();

    @WebMethod
    Type getType(@WebParam(name = "id") Long id);

    @WebMethod
    Type createType(@WebParam(name = "type") Type type);

    @WebMethod
    Type updateType(@WebParam(name = "id") Long id, @WebParam(name = "type") Type type);

    @WebMethod
    boolean deleteType(@WebParam(name = "id") Long id);
}
