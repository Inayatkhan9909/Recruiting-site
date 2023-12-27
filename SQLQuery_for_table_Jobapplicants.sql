
Create table Jobapplicants(Form_no int primary key identity(99223,132),
firstname varchar(100) not null,lastname varchar(100) not null,
email varchar(100) unique not null, qualification varchar(100) not null,
skills varchar(100) not null, job varchar(100) not null,
Created_at DATETIME not null default current_timestamp)

insert into Jobapplicants(firstname,lastname,email,qualification,skills,job)
values('Owais','Khan','owais@gmail.com','B-Tech','C#,Css,Html,ASP>NET','Front-End developer')

insert into Jobapplicants(firstname,lastname,email,qualification,skills,job)
values('Sahil','Mathor','mathor@gmail.com','D-Tech','javascript','Front-End developer')

insert into Jobapplicants(firstname,lastname,email,qualification,skills,job)
values('Tanvir','Magami','Magami@hotmail.com','J-Tech','C#,Css,Html','PHP developer')

insert into Jobapplicants(firstname,lastname,email,qualification,skills,job)
values('Mehvish','Tango','Pango@gmail.com','Z-Tech','Css,Html','Full=Stack developer')

select * from Jobapplicants
drop table Jobapplicants


