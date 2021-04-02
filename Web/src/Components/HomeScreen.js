import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import Button from "./Button";
import Table from "./Table";

const DATA = [
  {
    "Nombre del archivo": "Doc1",
    "Tipo de archivo": "PDF",
    "Fecha de subida": "12/10/2020",
    "Progreso de subida": "Listo",
  },
  {
    "Nombre del archivo": "Doc1.1",
    "Tipo de archivo": "Doc",
    "Fecha de subida": "30/10/2020",
    "Progreso de subida": "Listo",
  },
  {
    "Nombre del archivo": "Minuta",
    "Tipo de archivo": "TXT",
    "Fecha de subida": "12/3/2021",
    "Progreso de subida": 50,
  },
];

const Div = styled.div`
  display: flex;
  flex-direction: column;
  box-sizing: border-box;
  height: 100%;
  padding: 16px 32px;
`;

const HomeScreen = () => {
  const [data, setData] = useState(DATA);

  const insert = () => {
    /*const newItem = {
      "Nombre del archivo": "NUEVO",
      "Tipo de archivo": "TXT",
      "Fecha de subida": new Date().toISOString(),
      "Progreso de subida": 0,
    };
    setData([...data, newItem]);*/

    const id = setInterval(() => {
      setData((data) => {
        const newData = [...data];
        const index = data.findIndex(
          (el) => el["Nombre del archivo"] === "Minuta"
        );
        const newItem = { ...data[index] };
        if (newItem["Progreso de subida"] >= 100) {
          clearInterval(id);
          return data;
        }
        newItem["Progreso de subida"] += 10;
        newData[index] = newItem;
        return newData;
      });
    }, 1000);
  };

  return (
    <Div>
      <Link to="/login">Cerrar sesión</Link>
      <hr></hr>
      <Table data={data}></Table>
      <Button onClick={insert}>Nuevo archivo</Button>
    </Div>
  );
};

export default HomeScreen;
