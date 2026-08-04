package com.exemple.soap.config;

import com.exemple.soap.service.ProduitServiceImpl;
import com.exemple.soap.service.TypeServiceImpl;
import jakarta.xml.ws.Endpoint;
import org.apache.cxf.Bus;
import org.apache.cxf.jaxws.EndpointImpl;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class CxfConfig {

    @Autowired
    private Bus bus;

    @Autowired
    private TypeServiceImpl typeService;

    @Autowired
    private ProduitServiceImpl produitService;

    @Bean
    public Endpoint typeEndpoint() {
        EndpointImpl endpoint = new EndpointImpl(bus, typeService);
        endpoint.publish("/types");
        return endpoint;
    }

    @Bean
    public Endpoint produitEndpoint() {
        EndpointImpl endpoint = new EndpointImpl(bus, produitService);
        endpoint.publish("/produits");
        return endpoint;
    }
}
