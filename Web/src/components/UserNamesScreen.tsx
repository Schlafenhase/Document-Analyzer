import { useEffect, useState } from "react";
import styled from "styled-components";
import Button from "./UI/Button";
import Table from "./UI/Table";
import axios from "axios";
import homeBg from "../assets/homeBg.jpg";

const DATA = [
  {
    ID: 1,
    Nombre: "Francisco Solano",
  },
  {
    ID: 2,
    Nombre: "Pancho Gómez",
  },
  {
    ID: 3,
    Nombre: "Pepito",
  },
];

const Div = styled.div`
  box-sizing: border-box;
  height: 100%;
  padding: 32px;

  background-image: url(${homeBg});
  background-size: cover;
`;

const Container = styled.div`
  background-color: white;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
  max-width: 840px;
  margin: auto;
  border-radius: 8px;
  padding: 8px;
  height: 100%;
  box-sizing: border-box;
`;

const Row = styled.div`
  display: flex;
  justify-content: space-between;
`;

const UserNamesScreen = (props: any) => {
  const [data, setData] = useState(DATA);
  const [name, setName] = useState("");

  const getEmployees = async () => {
    const response = await axios.get(
      props.baseUrl + "/Api/Employee/Employees",
      {
        headers: {
          Authorization: "Bearer " + props.token,
        },
      }
    );
    setData(response.data.employees);
  };

  useEffect(() => {
    getEmployees();
  }, []);

  const postEmployee = async () => {
    const response = await axios.post(
      props.baseUrl + "/Api/Employee",
      { name: name },
      {
        headers: {
          Authorization: "Bearer " + props.token,
        },
      }
    );
    if (response.status === 200) {
      alert("Nombre agregado correctamente");
      setName("");
      setTimeout(getEmployees, 1000);
    } else {
      alert("Error en el servidor");
    }
  };

  return (
    <Div>
      <Container>
        <Row>
          <h1>Nombres de empleados</h1>
          <Button onClick={getEmployees}>Refrescar</Button>
        </Row>
        <Table data={data} />
        <div>
          <input
            placeholder="Nuevo usuario"
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <Button onClick={postEmployee}>Agregar</Button>
        </div>
      </Container>
    </Div>
  );
};

export default UserNamesScreen;
