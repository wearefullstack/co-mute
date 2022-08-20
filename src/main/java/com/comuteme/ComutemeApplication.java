package com.comuteme;

import com.comuteme.domain.Carpool;
import com.comuteme.domain.CoMuteUser;
import com.comuteme.repositories.CarPoolRepository;
import com.comuteme.repositories.CoMuteUserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.format.datetime.DateFormatter;

import static java.util.Arrays.asList;

@SpringBootApplication
public class ComutemeApplication implements CommandLineRunner {

	@Autowired
	private CarPoolRepository poolRepository;

	@Autowired
	private CoMuteUserRepository coMuteUserRepository;

	public static void main(String[] args) {
		SpringApplication.run(ComutemeApplication.class, args);
	}
	@Override
	public void run(String [] args) throws Exception{

		CoMuteUser coMuteUser = new CoMuteUser("James", "Franco", "test", "test1");
		CoMuteUser coMuteUser1 = new CoMuteUser("wendy", "baker", "test", "test2");

		coMuteUserRepository.saveAll(asList(coMuteUser, coMuteUser1));

		poolRepository.save(new Carpool("Jozi","08:30", "Tshwane", "11:30", 2, coMuteUser));
		poolRepository.save(new Carpool("CPT","08:30", "DBN", "11:30", 3, coMuteUser1));
		poolRepository.save(new Carpool("PE","08:30", "CHN", "11:30", 1));

	}

}
