package eit.masterschool.eu.ar_notes_server.controller;

import eit.masterschool.eu.ar_notes_server.model.Task;
import eit.masterschool.eu.ar_notes_server.repository.TaskRepository;
import eit.masterschool.eu.ar_notes_server.repository.UserRepository;
import io.swagger.annotations.Api;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;

@RestController
@Api(value = "tasks", description = "Operations for managing Tasks")
public class TaskController {

    @Autowired
    private TaskRepository taskRepository;

    @Autowired
    private UserRepository userRepository;

    @GetMapping("/tasks/all")
    public List<Task> getAllTasks() {
        return taskRepository.findAll();
    }

    @GetMapping("/tasks/{taskId}")
    public Task getTasksById(@PathVariable Long taskId) {
        return taskRepository.findTaskById(taskId);
    }


    @DeleteMapping("/tasks/delete/{taskId}")
    public ResponseEntity<?> deleteTask(@PathVariable Long taskId) {
        return taskRepository.findById(taskId)
                .map(task -> {
                    taskRepository.delete(task);
                    return ResponseEntity.ok().build();
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));

    }

    @PostMapping("/tasks/rename-task/{taskId}")
    public Task renameTask(@PathVariable Long taskId,
                           @Valid @RequestBody Task taskRequest) {
        return taskRepository.findById(taskId)
                .map(task -> {
                    task.setTitle(taskRequest.getTitle());
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));
    }

    @GetMapping("/users/{userId}/tasks")
    public List<Task> getTasksByUserId(@PathVariable Long userId) {
        return taskRepository.findTasksByUserId(userId);
    }


    @PostMapping("/users/{userId}/claim-task")
    public Task claimTask(@PathVariable Long userId,
                           @Valid @RequestBody Task task) {
        return userRepository.findById(userId)
                .map(user -> {
                    task.setUserId(userId);
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("User not found with id " + userId));
    }


    @PutMapping("/users/{userId}/tasks/update/{taskId}")
    public Task updateTaskForUser(@PathVariable Long userId,
                                  @PathVariable Long taskId,
                                  @Valid @RequestBody Task taskRequest) {
        if (!userRepository.existsById(userId)) {
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
                    task.setMarker(taskRequest.getMarker());
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));
    }

    @DeleteMapping("/users/{taskId}/tasks/delete/{taskId}")
    public ResponseEntity<?> deleteTaskForUser(@PathVariable Long userId,
                                               @PathVariable Long taskId) {
        if (!userRepository.existsById(userId)) {
            throw new ResourceNotFoundException("User not found with id " + userId);
        }

        return taskRepository.findById(taskId)
                .map(task -> {
                    taskRepository.delete(task);
                    return ResponseEntity.ok().build();
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));

    }


}