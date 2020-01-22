CREATE TABLE "User" (
	"id" serial NOT NULL,
	"firstName" VARCHAR(255) NOT NULL,
	"lastName" VARCHAR(255) NOT NULL,
	"username" VARCHAR(255) NOT NULL,
	"email" VARCHAR(255) NOT NULL,
	"password" VARCHAR(255) NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	"enabled" BOOLEAN NOT NULL,
	CONSTRAINT "User_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "UserProjectRole" (
	"id" serial NOT NULL,
	"roleId" integer NOT NULL,
	"userId" integer NOT NULL,
	"projectId" integer NOT NULL,
	CONSTRAINT "UserProjectRole_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Role" (
	"id" serial NOT NULL,
	"name" VARCHAR(255) NOT NULL,
	"description" VARCHAR(255) NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	"enabled" BOOLEAN NOT NULL,
	CONSTRAINT "Role_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Project" (
	"id" serial NOT NULL,
	"name" VARCHAR(255) NOT NULL,
	"description" VARCHAR(255) NOT NULL,
	"startDate" TIMESTAMP NOT NULL,
	"endDate" TIMESTAMP NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	"teamLeaderId" integer NOT NULL,
	CONSTRAINT "Project_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Task" (
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
	CONSTRAINT "Task_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Note" (
	"id" serial NOT NULL,
	"taskId" integer NOT NULL,
	"userId" integer NOT NULL,
	"text" VARCHAR(255) NOT NULL,
	"createdAt" TIMESTAMP NOT NULL,
	"updatedAt" TIMESTAMP NOT NULL,
	CONSTRAINT "Note_pk" PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);




ALTER TABLE "UserProjectRole" ADD CONSTRAINT "UserProjectRole_fk0" FOREIGN KEY ("roleId") REFERENCES "Role"("id");
ALTER TABLE "UserProjectRole" ADD CONSTRAINT "UserProjectRole_fk1" FOREIGN KEY ("userId") REFERENCES "User"("id");
ALTER TABLE "UserProjectRole" ADD CONSTRAINT "UserProjectRole_fk2" FOREIGN KEY ("projectId") REFERENCES "Project"("id");



ALTER TABLE "Task" ADD CONSTRAINT "Task_fk0" FOREIGN KEY ("userId") REFERENCES "User"("id");
ALTER TABLE "Task" ADD CONSTRAINT "Task_fk1" FOREIGN KEY ("projectId") REFERENCES "Project"("id");

ALTER TABLE "Note" ADD CONSTRAINT "Note_fk0" FOREIGN KEY ("taskId") REFERENCES "Task"("id");
ALTER TABLE "Note" ADD CONSTRAINT "Note_fk1" FOREIGN KEY ("userId") REFERENCES "User"("id");

