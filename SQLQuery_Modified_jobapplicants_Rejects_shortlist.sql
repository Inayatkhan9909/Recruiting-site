Create table Jobapplicants(Form_no int primary key identity(99223,132),
firstname varchar(100) not null,lastname varchar(100) not null,
email varchar(100) unique not null,dob varchar(100) not null,
qualification varchar(100) not null,
skills varchar(100) not null, job varchar(100) not null,
Created_at DATETIME not null default current_timestamp)

insert into Jobapplicants(firstname,lastname,email,dob,qualification,skills,job)
values('Owais','Khan','owais@gmail.com','01-01-2002','B-Tech','C#,Css,Html,ASP>NET','Front-End developer')

insert into Jobapplicants(firstname,lastname,email,dob,qualification,skills,job)
values ('Sahil','Mathor','mathor@gmail.com','21-03-2002','D-Tech','javascript','Front-End developer')

insert into Jobapplicants(firstname,lastname,email,dob,qualification,skills,job)
values('Tanvir','Magami','Magami@hotmail.com','11-11-2000','J-Tech','C#,Css,Html','PHP developer')

insert into Jobapplicants(firstname,lastname,email,dob,qualification,skills,job)
values('Mehvish','Tango','Pango@gmail.com','16-07-2002','Z-Tech','Css,Html','Full-Stack developer')

select * from Jobapplicants
drop table Jobapplicants
truncate table Jobapplicants


Create table Rejectedapplicants(Form_no int primary key ,
firstname varchar(100) not null,lastname varchar(100) not null,
email varchar(100) unique not null,dob varchar(50) not null, qualification varchar(100) not null,
skills varchar(100) not null, job varchar(100) not null,
Created_at DATETIME not null default current_timestamp)



INSERT INTO Rejectedapplicants (Form_no, firstname, lastname, email, qualification, skills, job)
SELECT Form_no, firstname, lastname, email, qualification, skills, job
FROM Jobapplicants
WHERE Form_No = 99223;

select * from Rejectedapplicants
drop table Rejectedapplicants


Create table Shortlistedapplicants(Form_no int primary key ,
firstname varchar(100) not null,lastname varchar(100) not null,
email varchar(100) unique not null,dob varchar(100) not null, qualification varchar(100) not null,
skills varchar(100) not null, job varchar(100) not null,
Created_at DATETIME not null default current_timestamp)



INSERT INTO Shortlistedapplicants (Form_no, firstname, lastname, email, qualification, skills, job)
SELECT Form_no, firstname, lastname, email, qualification, skills, job
FROM Jobapplicants
WHERE Form_No = 99223;

truncate table Shortlistedapplicants
select * from Shortlistedapplicants

drop table Shortlistedapplicants