package eit.masterschool.eu.ar_notes_server.repository;


import eit.masterschool.eu.ar_notes_server.model.Task;
import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

import java.util.List;

@RepositoryRestResource(collectionResourceRel = "tasks", path = "tasks")
public interface TaskRepository extends PagingAndSortingRepository<Task, Long> {

    List<Task> findByStatus(@Param("status") String status);

    List<Task> findAll();

}