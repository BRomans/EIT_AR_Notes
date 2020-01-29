package eit.masterschool.eu.ar_notes_server.model;

import javax.persistence.*;
import java.util.Date;

/**
 * Base model of a Task entity
 * @author BRomans.
 *
 */
@Entity
@Table(name = "tasks")
public class Task extends AuditModel {

    @Id
    @GeneratedValue
    private Long id;

    private Long userId;
    private Long projectId;
    private String title;
    private String description;
    private Date startDate;
    private Date endDate;
    private String status;
    private String marker;

    public Task() {

    }

    public Task(Long id, Long userId, Long projectId, String title, String description, Date startDate, Date endDate, String status, String marker) {
        this.id = id;
        this.userId = userId;
        this.projectId = projectId;
        this.title = title;
        this.description = description;
        this.startDate = startDate;
        this.endDate = endDate;
        this.status = status;
        this.marker = marker;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getUserId() {
        return userId;
    }

    public void setUserId(Long userId) {
        this.userId = userId;
    }

    public Long getProjectId() {
        return projectId;
    }

    public void setProjectId(Long projectId) {
        this.projectId = projectId;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Date getStartDate() {
        return startDate;
    }

    public void setStartDate(Date startDate) {
        this.startDate = startDate;
    }

    public Date getEndDate() {
        return endDate;
    }

    public void setEndDate(Date endDate) {
        this.endDate = endDate;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getMarker() {
        return marker;
    }

    public void setMarker(String marker) {
        this.marker = marker;
    }
}
