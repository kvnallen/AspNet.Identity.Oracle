


--
CREATE TABLE "SSO"."IDSSO_ROLES"
(
	"ID"   VARCHAR2(128 BYTE) NOT NULL,
	"NAME" VARCHAR2(256 BYTE) NOT NULL,
	"DESCRIPTION" VARCHAR2(1000 BYTE)
);
CREATE UNIQUE INDEX "SSO"."PK_IDSSO_ROLES_ID" ON "SSO"."IDSSO_ROLES" ("ID");
ALTER TABLE "SSO"."IDSSO_ROLES" ADD CONSTRAINT "PK_IDSSO_ROLES_ID" PRIMARY KEY ("ID");



--
CREATE TABLE "SSO"."IDSSO_USERS"
(
	"ID"             VARCHAR2(128 BYTE) NOT NULL,
	"EMAIL"          VARCHAR2(256 BYTE) DEFAULT NULL,
	"EMAILCONFIRMED" NUMBER(3, 0) NOT NULL,
	"PASSWORDHASH" CLOB,
	"SECURITYSTAMP" CLOB,
	"PHONENUMBER" CLOB,
	"PHONENUMBERCONFIRMED" NUMBER(3, 0) NOT NULL,
	"TWOFACTORENABLED"     NUMBER(3, 0) NOT NULL,
	"LOCKOUTENDDATEUTC"    DATE DEFAULT NULL,
	"LOCKOUTENABLED"       NUMBER(3, 0) NOT NULL,
	"ACCESSFAILEDCOUNT"    NUMBER(10, 0) NOT NULL,
	"USERNAME"             VARCHAR2(256 BYTE) NOT NULL,
	"ENABLED"			   NUMBER(3, 0) NOT NULL DEFAULT 1,
);
CREATE UNIQUE INDEX "SSO"."PK_IDSSO_USERS_ID" ON "SSO"."IDSSO_USERS" ("ID");
ALTER TABLE "SSO"."IDSSO_USERS" ADD CONSTRAINT "PK_IDSSO_USERS_ID" PRIMARY KEY ("ID");



--
CREATE TABLE "SSO"."IDSSO_USERCLAIMS"
(
	"ID"     NUMBER(10, 0) NOT NULL,
	"USERID" VARCHAR2(128 BYTE) NOT NULL,
	"CLAIMTYPE" CLOB,
	"CLAIMVALUE" CLOB
);
CREATE UNIQUE INDEX "SSO"."PK_IDSSO_USERCLAIMS_ID" ON "SSO"."IDSSO_USERCLAIMS" ("ID");
ALTER TABLE "SSO"."IDSSO_USERCLAIMS" ADD CONSTRAINT "PK_IDSSO_USERCLAIMS_ID" PRIMARY KEY ("ID");

CREATE INDEX "SSO"."FK_IDSSO_USERCLAIMS_USERID" ON "SSO"."IDSSO_USERCLAIMS" ("USERID");
ALTER TABLE "SSO"."IDSSO_USERCLAIMS" ADD CONSTRAINT "FK_IDSSO_USERCLAIMS_USERID" FOREIGN KEY ("USERID")
  REFERENCES "SSO"."IDSSO_USERS" ("ID") ON DELETE CASCADE ENABLE;

CREATE SEQUENCE "SSO"."SEQ_IDSSO_USERCLAIMS_ID"
START WITH 1
INCREMENT BY 1
NOMAXVALUE; 

CREATE OR REPLACE TRIGGER "SSO"."TR_IDSSO_USERCLAIMS_INS" 
	BEFORE INSERT
	ON "SSO"."IDSSO_USERCLAIMS"
	REFERENCING NEW AS new OLD AS old
	FOR EACH ROW
BEGIN
	IF INSERTING
	THEN
		IF :new.id IS NULL
		THEN
			SELECT SEQ_IDSSO_USERCLAIMS_ID.NEXTVAL INTO :new.id FROM DUAL;
		END IF;
	END IF;
END;
/
ALTER TRIGGER "SSO"."TR_IDSSO_USERCLAIMS_INS" ENABLE;



--
CREATE TABLE "SSO"."IDSSO_USERLOGINS"
(
	"LOGINPROVIDER" VARCHAR2(128 BYTE) NOT NULL,
	"PROVIDERKEY"   VARCHAR2(128 BYTE) NOT NULL,
	"USERID"        VARCHAR2(128 BYTE) NOT NULL
);
CREATE INDEX "SSO"."PK_IDSSO_USERLOGINS_USERID" ON "SSO"."IDSSO_USERLOGINS" ("LOGINPROVIDER","PROVIDERKEY","USERID");
ALTER TABLE "SSO"."IDSSO_USERLOGINS" ADD  PRIMARY KEY ("LOGINPROVIDER","PROVIDERKEY","USERID");

CREATE INDEX "SSO"."FK_IDSSO_USERLOGINS_USERID" ON "SSO"."IDSSO_USERLOGINS" ("USERID");
ALTER TABLE "SSO"."IDSSO_USERLOGINS" ADD CONSTRAINT "FK_IDSSO_USERLOGINS_USERID" FOREIGN KEY ("USERID")
  REFERENCES "SSO"."IDSSO_USERS" ("ID") ON DELETE CASCADE ENABLE;



--
CREATE TABLE "SSO"."IDSSO_USERROLES"
(
	"USERID" VARCHAR2(128 BYTE) NOT NULL,
	"ROLEID" VARCHAR2(128 BYTE) NOT NULL
) ;
CREATE UNIQUE INDEX "SSO"."PK_IDSSO_USERROLES_ID" ON "SSO"."IDSSO_USERROLES" ("USERID","ROLEID");
ALTER TABLE "SSO"."IDSSO_USERROLES" ADD CONSTRAINT "PK_IDSSO_USERROLES_USERID" PRIMARY KEY ("USERID","ROLEID");

CREATE INDEX "SSO"."FK_IDSSO_USERROLES_ROLEID" ON "SSO"."IDSSO_USERROLES" ("ROLEID");
ALTER TABLE "SSO"."IDSSO_USERROLES" ADD CONSTRAINT "FK_IDSSO_USERROLES_USERID" FOREIGN KEY ("USERID")
  REFERENCES "SSO"."IDSSO_USERS" ("ID") ON DELETE CASCADE ENABLE;

ALTER TABLE "SSO"."IDSSO_USERROLES" ADD CONSTRAINT "FK_IDSSO_USERROLES_ROLEID" FOREIGN KEY ("ROLEID")
  REFERENCES "SSO"."IDSSO_ROLES" ("ID") ON DELETE CASCADE ENABLE;

