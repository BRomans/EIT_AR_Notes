package eit.masterschool.eu.ar_notes_server.repository;


import eit.masterschool.eu.ar_notes_server.model.Task;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface TaskRepository extends JpaRepository<Task, Long> {

    List<Task> findTasksByUserId(@Param("userId") Long userId);

    Task findTaskById(@Param("taskId") Long taskId);

    List<Task> findAll();

    List<Task> findAllByOrderByIdAsc();


}