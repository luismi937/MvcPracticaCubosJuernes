-- Script SQL para la base de datos PracticaCubos en MySQL
-- Usuario: root
-- Password: mysql

-- Este script asume que la base de datos PracticaCubos ya existe
-- Si necesitas crearla, descomenta la siguiente línea:
-- CREATE DATABASE IF NOT EXISTS PracticaCubos;

USE PracticaCubos;

-- Las tablas ya existen, este script es solo de referencia

-- Tabla CUBOS (ya creada)
-- CREATE TABLE CUBOS (
--     id_cubo INT NOT NULL PRIMARY KEY,
--     nombre VARCHAR(500) NOT NULL,
--     modelo VARCHAR(500) NOT NULL,
--     marca VARCHAR(500) NOT NULL,
--     imagen VARCHAR(500) NOT NULL,
--     precio INT NOT NULL
-- );

-- Tabla COMPRA (ya creada)
-- CREATE TABLE COMPRA (
--     id_compra INT NOT NULL,
--     id_cubo INT NOT NULL,
--     cantidad INT NOT NULL,
--     precio INT NOT NULL,
--     fechapedido DATETIME NOT NULL
-- );

-- Consultar datos existentes
SELECT * FROM CUBOS;
SELECT * FROM COMPRA;

-- Nota: La aplicación genera automáticamente los IDs usando MAX(id) + 1
-- No es necesario usar AUTO_INCREMENT ya que las tablas no lo tienen configurado
