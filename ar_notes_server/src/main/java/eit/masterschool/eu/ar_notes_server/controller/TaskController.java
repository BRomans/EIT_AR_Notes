package eit.masterschool.eu.ar_notes_server.controller;

import eit.masterschool.eu.ar_notes_server.model.Task;
import eit.masterschool.eu.ar_notes_server.repository.TaskRepository;
import eit.masterschool.eu.ar_notes_server.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;

@RestController
public class TaskController {

    @Autowired
    private TaskRepository taskRepository;

    @Autowired
    private UserRepository userRepository;

    @GetMapping("/users/{userId}/tasks")
    public List<Task> getTasksByUserId(@PathVariable Long userId) {
        return taskRepository.findTasksByUserId(userId);
    }

    @PostMapping("/users/{userId}/tasks")
    public Task addTask(@PathVariable Long userId,
                            @Valid @RequestBody Task task) {
        return userRepository.findById(userId)
                .map(user -> {
                    task.setUserId(userId);
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("User not found with id " + userId));
    }

    @PutMapping("/users/{userId}/tasks/{taskId}")
    public Task updateTask(@PathVariable Long userId,
                               @PathVariable Long taskId,
                               @Valid @RequestBody Task taskRequest) {
        if(!userRepository.existsById(userId)) {
            throw new ResourceNotFoundException("User not found with id " + userId);
        }

        return taskRepository.findById(taskId)
                .map(task -> {
                    task.setUserId(taskRequest.getUserId());
                    task.setDescription(taskRequest.getDescription());
                    task.setTitle(taskRequest.getTitle());
                    task.setStatus(taskRequest.getStatus());
                    task.setStartDate(taskRequest.getStartDate());
                    task.setEndDate(taskRequest.getEndDate());
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));
    }

    @DeleteMapping("/users/{taskId}/tasks/{taskId}")
    public ResponseEntity<?> deleteTask(@PathVariable Long userId,
                                          @PathVariable Long taskId) {
        if(!userRepository.existsById(userId)) {
            throw new ResourceNotFoundException("User not found with id " + userId);
        }

        return taskRepository.findById(taskId)
                .map(task -> {
                    taskRepository.delete(task);
                    return ResponseEntity.ok().build();
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));

    }
}