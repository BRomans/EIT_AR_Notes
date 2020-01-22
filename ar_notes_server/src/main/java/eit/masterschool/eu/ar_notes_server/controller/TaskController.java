package eit.masterschool.eu.ar_notes_server.controller;

import eit.masterschool.eu.ar_notes_server.model.Task;
import org.springframework.stereotype.Repository;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.time.LocalDateTime;
import java.util.concurrent.atomic.AtomicLong;

@RestController
public class TaskController {

    private final AtomicLong counter = new AtomicLong();

    private Repository taskRepository;

    @GetMapping("/task")
    public Task getTask(@RequestParam(value = "name", defaultValue = "World") String name) {
        return new Task(counter.incrementAndGet(),
                12L,
                1L,
                "Task",
                "My test task",
                LocalDateTime.now(),
                LocalDateTime.now(),
                LocalDateTime.now(),
                LocalDateTime.now(),
                "created"
        );
    }
}
