
#先进行删除表 这样多次运行不报错!

drop table if exists student;
drop table if exists subject;
drop table if exists score;

#创建学生表
create table student(
no varchar(20) not null,
sex varchar(2)  not null,
class varchar(20) not null, 
name varchar(20) not null,
birthdate date  null,
schoolyear int(4) null,
birthplace varchar(20) null,
remarks text null,
unique(no),primary key(no)
);


#创建科目表
create table  subject(
id int(5) not null AUTO_INCREMENT,
name varchar(20) not null,
credits int(5) not null,
lesson int(5) not null,
teacher varchar(20) not null,
remarks text null,
unique(id),primary key(id)
);

#创建成绩表
create table score(
id int(5) not null AUTO_INCREMENT,
year int(5) not null,
sno varchar(20) not null, #学生NO
bid int(5) not null,    #科目编号
result int(50) not null,
remarks text null,
unique(id),primary key(id)
)
