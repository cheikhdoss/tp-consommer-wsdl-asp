package com.exemple.soap.service;

import com.exemple.soap.model.Produit;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import jakarta.jws.WebService;

import java.util.List;

@WebService
public interface ProduitServiceSEI {

    @WebMethod
    List<Produit> getAllProduits();

    @WebMethod
    Produit getProduit(@WebParam(name = "id") Long id);

    @WebMethod
    Produit createProduit(@WebParam(name = "produit") Produit produit);

    @WebMethod
    Produit updateProduit(@WebParam(name = "id") Long id, @WebParam(name = "produit") Produit produit);

    @WebMethod
    boolean deleteProduit(@WebParam(name = "id") Long id);
}
