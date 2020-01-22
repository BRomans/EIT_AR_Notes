package eit.masterschool.eu.ar_notes_server;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.data.jpa.repository.config.EnableJpaAuditing;

@SpringBootApplication
@EnableJpaAuditing
public class ArNotesServerApplication {

	public static void main(String[] args) {
		SpringApplication.run(ArNotesServerApplication.class, args);
	}

}
