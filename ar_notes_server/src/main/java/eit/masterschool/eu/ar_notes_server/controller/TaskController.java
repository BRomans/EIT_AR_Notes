package eit.masterschool.eu.ar_notes_server.controller;

import eit.masterschool.eu.ar_notes_server.model.Task;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.time.LocalDateTime;
import java.util.concurrent.atomic.AtomicLong;

@RestController
public class TaskController {

    private static final String template = "Hello, %s!";
    private final AtomicLong counter = new AtomicLong();

    @GetMapping("/task")
    public Task getTask(@RequestParam(value = "name", defaultValue = "World") String name) {
        return new Task(String.valueOf(Long.valueOf(counter.incrementAndGet())),
                "test",
                "board",
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
