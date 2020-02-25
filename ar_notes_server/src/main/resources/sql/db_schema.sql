CREATE TABLE "users" (
	"id" serial NOT NULL,
	"first_name" VARCHAR(255) NOT NULL,
	"last_name" VARCHAR(255) NOT NULL,
	"username" VARCHAR(255) NOT NULL,
	"email" VARCHAR(255) NOT NULL,
	"password" VARCHAR(255) NOT NULL,
	"created_at" TIMESTAMP NOT NULL,
	"updated_at" TIMESTAMP NOT NULL,
	"enabled" BOOLEAN NOT NULL,
	CONSTRAINT "user_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "user_project_role" (
	"id" serial NOT NULL,
	"role_id" integer NOT NULL,
	"user_id" integer NOT NULL,
	"project_id" integer NOT NULL,
	CONSTRAINT "user_project_role_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "roles" (
	"id" serial NOT NULL,
	"name" VARCHAR(255) NOT NULL,
	"description" VARCHAR(255) NOT NULL,
	"created_at" TIMESTAMP NOT NULL,
	"updated_at" TIMESTAMP NOT NULL,
	"enabled" BOOLEAN NOT NULL,
	CONSTRAINT "roles_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "projects" (
	"id" serial NOT NULL,
	"name" VARCHAR(255) NOT NULL,
	"description" VARCHAR(255) NOT NULL,
	"start_date" TIMESTAMP NOT NULL,
	"end_date" TIMESTAMP NOT NULL,
	"created_at" TIMESTAMP NOT NULL,
	"updated_at" TIMESTAMP NOT NULL,
	"team_leader_id" integer NOT NULL,
	CONSTRAINT "projects_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "tasks" (
	"id" serial NOT NULL,
	"user_id" integer,
	"project_id" integer NOT NULL,
	"title" VARCHAR(255) NOT NULL,
	"description" VARCHAR(255) NOT NULL,
	"start_date" TIMESTAMP NOT NULL,
	"end_date" TIMESTAMP NOT NULL,
	"created_at" TIMESTAMP NOT NULL,
	"updated_at" TIMESTAMP NOT NULL,
	"status" VARCHAR(255) NOT NULL,
  "marker" VARCHAR(255) NOT NULL,
  "isEmpty" BOOLEAN NOT NULL,
	CONSTRAINT "tasks_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "notes" (
	"id" serial NOT NULL,
	"task_id" integer NOT NULL,
	"user_id" integer NOT NULL,
	"text" VARCHAR(255) NOT NULL,
	"created_at" TIMESTAMP NOT NULL,
	"updated_at" TIMESTAMP NOT NULL,
	CONSTRAINT "notes_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);




ALTER TABLE "user_project_role" ADD CONSTRAINT "user_project_role_fk0" FOREIGN KEY ("role_id") REFERENCES "roles"("id");
ALTER TABLE "user_project_role" ADD CONSTRAINT "user_project_role_fk1" FOREIGN KEY ("user_id") REFERENCES "users"("id");
ALTER TABLE "user_project_role" ADD CONSTRAINT "user_project_role_fk2" FOREIGN KEY ("project_id") REFERENCES "projects"("id");



ALTER TABLE "tasks" ADD CONSTRAINT "tasks_fk0" FOREIGN KEY ("user_id") REFERENCES "users"("id");
ALTER TABLE "tasks" ADD CONSTRAINT "tasks_fk1" FOREIGN KEY ("project_id") REFERENCES "projects"("id");

ALTER TABLE "notes" ADD CONSTRAINT "notes_fk0" FOREIGN KEY ("task_id") REFERENCES "tasks"("id");
ALTER TABLE "notes" ADD CONSTRAINT "notes_fk1" FOREIGN KEY ("user_id") REFERENCES "users"("id");

