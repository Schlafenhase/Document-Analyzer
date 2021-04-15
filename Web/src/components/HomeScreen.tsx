import { useContext, useEffect, useState } from "react";
import styled from "styled-components";

import Table from "./UI/Table";
import ContainerList from "../azure-storage/components/ContainerList";
import SelectedContainer from "../azure-storage/components/SelectedContainer";
import InputFile from "../azure-storage/components/InputFile";
import ItemsList from "../azure-storage/components/ItemsList";
import ItemsUploaded from "../azure-storage/components/ItemsUploaded";
import ItemsDownloaded from "../azure-storage/components/ItemsDownloaded";
import ItemsDeleted from "../azure-storage/components/ItemsDeleted";
import {
  SharedViewStateContext,
  UploadsViewStateContext,
} from "../azure-storage/contexts/viewStateContext";
import { tap } from "rxjs/operators";
import { ContainerItem } from "@azure/storage-blob";
import FilesBg from "../assets/FilesBg.jpg";
import { Container } from "./UI/Container";
import Button from "./UI/Button";
import axios from "axios";
import { BaseURL } from "../constants";

// const DATA = [
//   {
//     "Nombre del archivo": "Doc1",
//     "Fecha de subida": "12/10/2020",
//     "Progreso de subida": "Listo",
//   },
//   {
//     "Nombre del archivo": "Doc1.1",
//     "Fecha de subida": "30/10/2020",
//     "Progreso de subida": "Listo",
//   },
//   {
//     "Nombre del archivo": "Minuta",
//     "Fecha de subida": "12/3/2021",
//     "Progreso de subida": 50,
//   },
// ];

const Div = styled.div`
  box-sizing: border-box;
  height: 100%;
  padding: 32px;

  background-image: url(${FilesBg});
  background-size: cover;
`;

const Row = styled.div`
  display: flex;
  justify-content: space-between;
`;

const HomeScreen = (props: any) => {
  const [data, setData] = useState([]);
  const [fileData, setDataFile]: any = useState({});
  const [status, setStatus]: any = useState();

  const getFiles = async () => {
    const response = await axios.get(BaseURL + "/Api/File/Files", {
      headers: {
        Authorization: "Bearer " + props.token,
      },
    });
    setData(response.data.files);
  };

  useEffect(() => {
    if (props.token) getFiles();
  }, [props.token]);

  const getDetail = async (file: any) => {
    const response = await axios.get(BaseURL + "/Api/Mongo/" + file.id, {
      headers: {
        Authorization: "Bearer " + props.token,
      },
    });
    const dataFile = Object.keys(response.data.employees).map((key) => ({
      name: key,
      count: response.data.employees[key],
    })) as any;
    setDataFile({ title: file.name, data: dataFile });
  };

  const uploadFile = async (name: String) => {
    setStatus("procesando NLP...");
    const response = await axios.post(
      BaseURL + "/Api/NLP",
      { name },
      {
        headers: {
          Authorization: "Bearer " + props.token,
        },
      }
    );
    console.log(response.data);
    setStatus("Completo");
    setTimeout(getFiles, 1000);
    setTimeout(setStatus, 2000);
  };

  const viewContext = useContext(SharedViewStateContext);

  useEffect(() => {
    viewContext.getContainerItems("dcanalyzerblob");
  }, []);

  if (!props.token) {
    return null;
  }

  return (
    <Div>
      <Container>
        <Row>
          <h1>Archivos guardados</h1>
          <Button onClick={getFiles}>Refrescar</Button>
        </Row>
        <Table onClickItem={getDetail} data={data} />

        {status ? (
          <p>STATUS: {status}</p>
        ) : (
          <InputFile
            start={() => setStatus("subiendo a Azure...")}
            uploaded={uploadFile}
          />
        )}

        {fileData.title ? (
          [<h2>{fileData.title}</h2>, <Table data={fileData.data} />]
        ) : (
          <p>Seleccione un nombre para ver detalles</p>
        )}

        <div style={{ display: "none" }}>
          <hr />
          <hr />
          <h2>Debug Components - Delete Them Later:</h2>
          <ContainerList />
          <hr />
          <SelectedContainer className="container" />
          <ItemsList />

          <div className="item-details">
            <ItemsUploaded />
            <ItemsDownloaded />
            <ItemsDeleted />
          </div>
        </div>
      </Container>
    </Div>
  );
};

export default HomeScreen;
