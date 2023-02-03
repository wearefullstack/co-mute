package io.emmanuel.comutefinal.Repository;


import io.emmanuel.comutefinal.Model.CarPool;
import org.springframework.data.jpa.repository.JpaRepository;

public interface CarPoolRepository extends JpaRepository<CarPool, Long> {

    /*
    An @Query has to be created for the query which will return the list of car pool
    associated by userid

    List<CarPool> findByUserid(int userid);
     */
}