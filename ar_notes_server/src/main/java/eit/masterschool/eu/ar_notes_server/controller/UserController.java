package eit.masterschool.eu.ar_notes_server.controller;

import eit.masterschool.eu.ar_notes_server.model.User;
import eit.masterschool.eu.ar_notes_server.repository.UserRepository;
import io.swagger.annotations.Api;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;

/**
 * Controller for REST endpoints of Task entity.
 * @author BRomans.
 *
 */
@RestController
@Api(value="users", description="Operations for managing Users")
public class UserController {

    @Autowired
    private UserRepository userRepository;

    /**
     * Get all users with a paged option
     * @param pageable
     * @return a page of users
     */
    @GetMapping("/users/all/paged")
    public Page<User> getUsers(Pageable pageable) {
        return userRepository.findAll((pageable));
    }

    /**
     * Get a single user by its id
     * @param userId
     * @return a User
     */
    @GetMapping("/users/{userId}")
    public User getUserById(@PathVariable Long userId) {
        return userRepository.getUserById((userId));
    }

    /**
     * Get all the users
     * @return a list of Users
     */
    @GetMapping("/users/all")
    public List<User> getUsers() {
        return userRepository.findAll();
    }

    /**
     * Create a new User
     * @param user
     * @return the new User
     */
    @PostMapping("/users/create")
    public User createUser(@Valid @RequestBody User user) {
        return userRepository.save(user);
    }

    /**
     * Update an existing User by its id
     * @param userId
     * @param userRequest
     * @return the updated User
     */
    @PutMapping("/users/update/{userId}")
    public User updateUser(@PathVariable Long userId,
                                   @Valid @RequestBody User userRequest) {
        return userRepository.findById(userId)
                .map(user -> {
                    user.setUsername(userRequest.getUsername());
                    user.setEmail(userRequest.getEmail());
                    user.setPassword(userRequest.getPassword());
                    user.setEnabled(userRequest.isEnabled());
                    return userRepository.save(user);
                }).orElseThrow(() -> new ResourceNotFoundException("User not found with id " + userId));
    }


    /**
     * Delete an existing User by its id
     * @param userId
     * @return a response entity
     */
    @DeleteMapping("/users/delete/{userId}")
    public ResponseEntity<?> deleteUser(@PathVariable Long userId) {
        return userRepository.findById(userId)
                .map(user -> {
                    userRepository.delete(user);
                    return ResponseEntity.ok().build();
                }).orElseThrow(() -> new ResourceNotFoundException("User not found with id " + userId));
    }
}