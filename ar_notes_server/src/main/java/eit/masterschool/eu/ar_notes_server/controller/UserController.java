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

@RestController
@Api(value="users", description="Operations for managing Users")
public class UserController {

    @Autowired
    private UserRepository userRepository;

    @GetMapping("/users/all")
    public Page<User> getUsers(Pageable pageable) {
        return userRepository.findAll((pageable));
    }


    @PostMapping("/users/create")
    public User createUser(@Valid @RequestBody User user) {
        return userRepository.save(user);
    }

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


    @DeleteMapping("/users/delete/{userId}")
    public ResponseEntity<?> deleteUser(@PathVariable Long userId) {
        return userRepository.findById(userId)
                .map(user -> {
                    userRepository.delete(user);
                    return ResponseEntity.ok().build();
                }).orElseThrow(() -> new ResourceNotFoundException("User not found with id " + userId));
    }
}