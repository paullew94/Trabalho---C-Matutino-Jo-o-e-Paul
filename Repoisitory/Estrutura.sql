CREATE TABLE estados(
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(50),
sigla VARCHAR(2)
);


CREATE TABLE cidades(
id INT PRIMARY KEY IDENTITY(1,1),
id_estado INT,
FOREIGN KEY(id_estado) REFERENCES estados(id),

nome VARCHAR(50),
numero_habitantes INT
);

SELECT cidades.id AS 'CidadesId',
cidades.nome AS 'CidadeNome',
cidades.numero_habitantes AS 'CidadeNumeroHabitantes',
cidades.id_estado AS 'CidadeIdEstado',
estados.nome AS'EstadoNome'
FROM cidades INNER JOIN estados ON (cidades.id_estado = estados.id);

DROP TABLE clientes;
CREATE TABLE clientes(
id INT PRIMARY KEY IDENTITY(1,1),
id_cidade INT,
FOREIGN KEY(id_cidade) REFERENCES cidades(id),
nome VARCHAR(50),
cpf VARCHAR(50),
data_nascimento DATETIME2,
numero INT,
complemento NCHAR(10),
logradouro NCHAR(10),
cep NCHAR(10)
);

SELECT clientes.id AS 'ClientesId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',
clientes.data_nascimento AS 'ClienteDataNascimento',
clientes.numero AS 'ClienteNumero',
clientes.complemento AS 'ClienteComplemento',
clientes.logradouro AS 'ClienteLogradouro',
clientes.cep AS 'ClienteCep',
clientes.id_cidade AS 'ClienteIdCidade',
cidades.nome AS'CidadeNome'
FROM clientes INNER JOIN cidades ON (clientes.id_cidade = cidades.id);

SELECT*FROM cidades;

SELECT * FROM estados;