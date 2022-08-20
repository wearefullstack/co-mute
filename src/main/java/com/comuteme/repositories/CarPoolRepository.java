package com.comuteme.repositories;

import com.comuteme.domain.Carpool;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface CarPoolRepository  extends CrudRepository<Carpool, Long> {

    List<Carpool> findByOrigin(@Param("origin") String origin );
    List<Carpool> findByDestination(@Param("destination") String destination );
}
