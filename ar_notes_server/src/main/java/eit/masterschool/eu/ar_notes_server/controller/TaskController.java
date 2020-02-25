package eit.masterschool.eu.ar_notes_server.controller;

import eit.masterschool.eu.ar_notes_server.model.Task;
import eit.masterschool.eu.ar_notes_server.repository.TaskRepository;
import eit.masterschool.eu.ar_notes_server.repository.UserRepository;
import io.swagger.annotations.Api;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.Date;
import java.util.List;


/**
 * Controller for REST endpoints of Task entity.
 * @author BRomans.
 *
 */
@RestController
@Api(value = "tasks", description = "Operations for managing Tasks")
public class TaskController {

    @Autowired
    private TaskRepository taskRepository;

    @Autowired
    private UserRepository userRepository;

    /**
     * Get all the tasks in ascending order by their id.
     * @return all the tasks
     */
    @GetMapping("/tasks/all")
    public List<Task> getAllTasks() {
        return taskRepository.findAllByOrderByIdAsc();
    }

    /**
     * Get a task by its id
     * @param taskId
     * @return a task
     */
    @GetMapping("/tasks/{taskId}")
    public Task getTasksById(@PathVariable Long taskId) {
        return taskRepository.findTaskById(taskId);
    }

    /**
     * Create a new Task
     * @param task
     * @return the new Task
     */
    @PostMapping("/tasks/create")
    public Task createTask(@Valid @RequestBody Task task) {
        return taskRepository.save(task);
    }

    /**
     * Delete a task using its id
     * @param taskId
     * @return a response entity
     */
    @DeleteMapping("/tasks/delete/{taskId}")
    public ResponseEntity<?> deleteTask(@PathVariable Long taskId) {
        return taskRepository.findById(taskId)
                .map(task -> {
                    taskRepository.delete(task);
                    return ResponseEntity.ok().build();
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));

    }

    /**
     * Change the title of a task using its id
     * @param taskId
     * @param taskRequest
     * @return the updated task
     */
    @PostMapping("/tasks/rename-task/{taskId}")
    public Task renameTask(@PathVariable Long taskId,
                           @Valid @RequestBody Task taskRequest) {
        return taskRepository.findById(taskId)
                .map(task -> {
                    task.setTitle(taskRequest.getTitle());
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));
    }


    /**
     * Updates all the fields of a task using its id
     * @param taskId
     * @param taskRequest
     * @return the updated task
     */
    @PutMapping("/tasks/update/{taskId}")
    public Task updateTask(@PathVariable Long taskId,
                                  @Valid @RequestBody Task taskRequest) {
        return taskRepository.findById(taskId)
                .map(task -> {
                    task.setUserId(taskRequest.getUserId());
                    task.setDescription(taskRequest.getDescription());
                    task.setTitle(taskRequest.getTitle());
                    task.setStatus(taskRequest.getStatus());
                    task.setStartDate(taskRequest.getStartDate());
                    task.setEndDate(taskRequest.getEndDate());
                    task.setMarker(taskRequest.getMarker());
                    task.setEmpty(taskRequest.isEmpty());
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));
    }

    /**
     * Updates all the fields of a task using its id
     * @param taskId
     * @param taskRequest
     * @return the updated task
     */
    @PutMapping("/tasks/clear/{taskId}")
    public Task clearTask(@PathVariable Long taskId,
                           @Valid @RequestBody Task taskRequest) {
        return taskRepository.findById(taskId)
                .map(task -> {
                    task.setUserId(taskRequest.getUserId());
                    task.setDescription("");
                    task.setTitle(taskRequest.getTitle());
                    task.setStatus("todo");
                    task.setStartDate(new Date());
                    task.setEndDate(new Date());
                    task.setMarker(taskRequest.getMarker());
                    task.setEmpty(true);
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));
    }

    /**
     * Get all tasks assigned to a specific user
     * @param userId
     * @return a list of tasks
     */
    @GetMapping("/users/{userId}/tasks")
    public List<Task> getTasksByUserId(@PathVariable Long userId) {
        return taskRepository.findTasksByUserId(userId);
    }


    /**
     * Claim a task for the current user
     * @param userId
     * @param task
     * @return the claimed task
     */
    @PostMapping("/users/{userId}/claim-task")
    public Task claimTask(@PathVariable Long userId,
                           @Valid @RequestBody Task task) {
        return userRepository.findById(userId)
                .map(user -> {
                    task.setUserId(userId);
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("User not found with id " + userId));
    }


    /**
     * Update a task assigned to a specific user
     * @param userId
     * @param taskId
     * @param taskRequest
     * @return the assigned task
     */
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
                    task.setEmpty(taskRequest.isEmpty());
                    return taskRepository.save(task);
                }).orElseThrow(() -> new ResourceNotFoundException("Task not found with id " + taskId));
    }

    /**
     * Delete a task assigned to a specific user
     * @param userId
     * @param taskId
     * @return a response entity
     */
    @DeleteMapping("/users/{userId}/tasks/delete/{taskId}")
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