USE jocs_dam05;

CREATE TABLE roles (
    id_rol INT IDENTITY(1,1) PRIMARY KEY,
    nombre_rol VARCHAR(15) NOT NULL
);

CREATE TABLE usuarios (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    nombre_usuario VARCHAR(25) NOT NULL,
    hash_contr NVARCHAR(255) NOT NULL,
    id_rol INT NOT NULL,
    fecha_registro DATETIME NOT NULL,
    CONSTRAINT FK_usuarios_roles FOREIGN KEY (id_rol) REFERENCES roles(id_rol)
);

CREATE TABLE preguntas (
    id_pregunta INT IDENTITY(1,1) PRIMARY KEY,
    nivel_dificultad INT NOT NULL,
    numero_pregunta INT NOT NULL,
    total_aciertos INT NOT NULL,
    total_errores INT NOT NULL
);

CREATE TABLE partidas (
    id_partida INT IDENTITY(1,1) PRIMARY KEY,
    id_usuario INT NOT NULL,
    fecha_partida DATETIME NOT NULL,
    duracion INT NOT NULL,
    nivel_dificultad INT NOT NULL,
    estrellas INT NOT NULL,
    CONSTRAINT FK_partidas_usuarios FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario)
);

CREATE TABLE preguntas_partidas (
    id_partida INT NOT NULL,
    id_pregunta INT NOT NULL,
    acierto TINYINT NOT NULL,
    PRIMARY KEY (id_partida, id_pregunta),
    CONSTRAINT FK_preguntas_partidas_partidas FOREIGN KEY (id_partida) REFERENCES partidas(id_partida),
    CONSTRAINT FK_preguntas_partidas_preguntas FOREIGN KEY (id_pregunta) REFERENCES preguntas(id_pregunta)
);

INSERT INTO roles (nombre_rol) VALUES ('admin');
INSERT INTO roles (nombre_rol) VALUES ('jugador');

INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (1, 1, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (1, 2, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (1, 3, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (1, 4, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (1, 5, 0, 0);

INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (2, 1, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (2, 2, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (2, 3, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (2, 4, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (2, 5, 0, 0);

INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (3, 1, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (3, 2, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (3, 3, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (3, 4, 0, 0);
INSERT INTO preguntas (nivel_dificultad, numero_pregunta, total_aciertos, total_errores) VALUES (3, 5, 0, 0);