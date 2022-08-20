package com.comuteme.repositories;

import com.comuteme.domain.CoMuteUser;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;

public interface CoMuteUserRepository extends CrudRepository<CoMuteUser, Long> {

    CoMuteUser findByEmail(@Param("email") String email );

}
