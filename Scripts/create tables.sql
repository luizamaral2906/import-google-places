/*
drop table DiasDaSemana;
drop table HorariosFuncionamento;
drop table HorariosHistoricoHistogram;
drop table ReviewsUsuarios;
drop table ImagemURL;
drop table Local;

delete from HorariosFuncionamento;
delete from HorariosHistoricoHistogram;
delete from ReviewsUsuarios;
delete from ImagemURL;
delete from Local;
*/



create table DiasDaSemana
(
	idDiaSemana int not null primary key,
	diaDesc varchar(30)
)
insert into DiasDaSemana (idDiaSemana, diaDesc) values (1, 'Domingo');
insert into DiasDaSemana (idDiaSemana, diaDesc) values (2, 'Segunda-Feira');
insert into DiasDaSemana (idDiaSemana, diaDesc) values (3, 'Terça-Feira');
insert into DiasDaSemana (idDiaSemana, diaDesc) values (4, 'Quarta-Feira');
insert into DiasDaSemana (idDiaSemana, diaDesc) values (5, 'Quinta-Feira');
insert into DiasDaSemana (idDiaSemana, diaDesc) values (6, 'Sexta-Feira');
insert into DiasDaSemana (idDiaSemana, diaDesc) values (7, 'Sabado');


create table Local
(
	idLocal int primary key GENERATED ALWAYS AS IDENTITY,
	titulo varchar(300),
	pontuacaoTotal numeric,
	categoriaNome varchar(300),
	endereco varchar(500),
	plusCode varchar (300),
	website varchar (300),
	telefone varchar (300),
	temporarioFechado bool,
    fechado bool,
	ranking bigint,
    placeId varchar(100),
    url varchar(700),
	latitude numeric,
	longitude numeric,
    searchString varchar(300),
    HorarioPopularesTxt varchar(500),
	contagemReviews bigint
);


create table HorariosFuncionamento
(
	idHorariosFuncionamento int primary key GENERATED ALWAYS AS IDENTITY,
	idLocal int not null,
	idDiaSemana int not null,
	intervaloDesc varchar(100) not null,
	
	CONSTRAINT fk_DiasDaSemana
      FOREIGN KEY(idDiaSemana) 
	  REFERENCES DiasDaSemana(idDiaSemana),
	  
	CONSTRAINT fk_Local
      FOREIGN KEY(idLocal) 
	  REFERENCES Local(idLocal)
);


create table HorariosHistoricoHistogram
(
	idHorariosHistorico int primary key GENERATED ALWAYS AS IDENTITY,
	idLocal int not null,
	idDiaSemana int not null,
	hora int not null,
	taxaOcupacao int not null,
	
	CONSTRAINT fk_DiasDaSemana
      FOREIGN KEY(idDiaSemana) 
	  REFERENCES DiasDaSemana(idDiaSemana),
	  
	CONSTRAINT fk_Local
      FOREIGN KEY(idLocal) 
	  REFERENCES Local(idLocal)
);

create table ReviewsUsuarios
(
	idReviewsUsuarios int primary key GENERATED ALWAYS AS IDENTITY,
	idLocal int,
	idReview varchar(100),
	tempoPublicacaoDataImport varchar(300),
	dataImport date,
	url varchar(1000),
	likes int,
	pontuacao int,
	nome varchar(500),
	mensagem varchar(10000),
    reviewerId varchar(300),
    reviewerUrl varchar(1000),
    reviewerNumberOfReviews int,
    isLocalGuide bool,

	CONSTRAINT fk_Local_ReviewsUsuarios
      FOREIGN KEY(idLocal) 
	  REFERENCES Local(idLocal)
);

create table ImagemURL
(
	idImagemURL int primary key GENERATED ALWAYS AS IDENTITY,
	idLocal int not null,
	url varchar(1000) not null,
	  
	CONSTRAINT fk_Local
      FOREIGN KEY(idLocal) 
	  REFERENCES Local(idLocal)
);

select *from local where idlocal = 3227

select i.*from reviewsusuarios i
inner join local l
	on i.idlocal = l.idlocal
where i.idlocal = 3227

select *from ImagemURL i
inner join local l
	on i.idlocal = l.idlocal
where i.idlocal = 3227

