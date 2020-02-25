INSERT INTO public.tasks(
	user_id, project_id, title, description, start_date, end_date, created_at, updated_at, status, marker, isEmpty)
	VALUES (1, 1, 'Task 1', 'Sample description', NOW(), NOW(), NOW(), NOW(), 'in_progress',  'target_1', false);
INSERT INTO public.tasks(
	user_id, project_id, title, description, start_date, end_date, created_at, updated_at, status, marker, isEmpty)
	VALUES (2, 1, 'Task 2', 'Sample description', NOW(), NOW(), NOW(), NOW(), 'todo',  'target_2', false);
INSERT INTO public.tasks(
	user_id, project_id, title, description, start_date, end_date, created_at, updated_at, status, marker, isEmpty)
	VALUES (3, 1, 'Task 3', 'Sample description', NOW(), NOW(), NOW(), NOW(), 'in_progress',  'target_3', false);
INSERT INTO public.tasks(
	user_id, project_id, title, description, start_date, end_date, created_at, updated_at, status, marker, isEmpty)
	VALUES (null, 1, 'Task 4', 'Sample description', NOW(), NOW(), NOW(), NOW(), 'in_progress',  'target_4', false);