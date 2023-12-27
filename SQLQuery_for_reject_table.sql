
Create table Rejectedapplicants(Form_no int primary key ,
firstname varchar(100) not null,lastname varchar(100) not null,
email varchar(100) unique not null, qualification varchar(100) not null,
skills varchar(100) not null, job varchar(100) not null,
Created_at DATETIME not null default current_timestamp)



INSERT INTO Rejectedapplicants (Form_no, firstname, lastname, email, qualification, skills, job)
SELECT Form_no, firstname, lastname, email, qualification, skills, job
FROM Jobapplicants
WHERE Form_No = 99223;

select * from Rejectedapplicants