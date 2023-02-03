package io.emmanuel.comutefinal;

import io.emmanuel.comutefinal.Repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class CoMuteFinalApplication {

	@Autowired
	UserRepository repository;
	public static void main(String[] args) {
		SpringApplication.run(CoMuteFinalApplication.class, args);
	}

}
