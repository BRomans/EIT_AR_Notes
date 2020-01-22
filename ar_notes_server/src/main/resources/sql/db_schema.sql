CREATE TABLE "users" (
	"id" serial NOT NULL,
	"firstName" VARCHAR(255) NOT NULL,
	"lastName" VARCHAR(255) NOT NULL,
	"username" VARCHAR(255) NOT NULL,
	"email" VARCHAR(255) NOT NULL,
	"password" VARCHAR(255) NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	"enabled" BOOLEAN NOT NULL,
	CONSTRAINT "user_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "user_project_role" (
	"id" serial NOT NULL,
	"roleId" integer NOT NULL,
	"userId" integer NOT NULL,
	"projectId" integer NOT NULL,
	CONSTRAINT "user_project_role_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "roles" (
	"id" serial NOT NULL,
	"name" VARCHAR(255) NOT NULL,
	"description" VARCHAR(255) NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	"enabled" BOOLEAN NOT NULL,
	CONSTRAINT "roles_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "projects" (
	"id" serial NOT NULL,
	"name" VARCHAR(255) NOT NULL,
	"description" VARCHAR(255) NOT NULL,
	"startDate" TIMESTAMP NOT NULL,
	"endDate" TIMESTAMP NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	"teamLeaderId" integer NOT NULL,
	CONSTRAINT "projects_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "tasks" (
	"id" serial NOT NULL,
	"userId" integer NOT NULL,
	"projectId" integer NOT NULL,
	"title" VARCHAR(255) NOT NULL,
	"description" VARCHAR(255) NOT NULL,
	"startDate" TIMESTAMP NOT NULL,
	"endDate" TIMESTAMP NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	"status" VARCHAR(255) NOT NULL,
	CONSTRAINT "tasks_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "notes" (
	"id" serial NOT NULL,
	"taskId" integer NOT NULL,
	"userId" integer NOT NULL,
	"text" VARCHAR(255) NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	CONSTRAINT "notes_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);




ALTER TABLE "user_project_role" ADD CONSTRAINT "user_project_role_fk0" FOREIGN KEY ("roleId") REFERENCES "roles"("id");
ALTER TABLE "user_project_role" ADD CONSTRAINT "user_project_role_fk1" FOREIGN KEY ("userId") REFERENCES "users"("id");
ALTER TABLE "user_project_role" ADD CONSTRAINT "user_project_role_fk2" FOREIGN KEY ("projectId") REFERENCES "projects"("id");



ALTER TABLE "tasks" ADD CONSTRAINT "tasks_fk0" FOREIGN KEY ("userId") REFERENCES "users"("id");
ALTER TABLE "tasks" ADD CONSTRAINT "tasks_fk1" FOREIGN KEY ("projectId") REFERENCES "projects"("id");

ALTER TABLE "notes" ADD CONSTRAINT "notes_fk0" FOREIGN KEY ("taskId") REFERENCES "tasks"("id");
ALTER TABLE "notes" ADD CONSTRAINT "notes_fk1" FOREIGN KEY ("userId") REFERENCES "users"("id");

