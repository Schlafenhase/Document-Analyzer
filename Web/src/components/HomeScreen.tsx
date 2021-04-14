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

  const getFiles = async () => {
    const response = await axios.get(BaseURL + "/Api/File/Files", {
      headers: {
        Authorization: "Bearer " + props.token,
      },
    });
    setData(response.data.files);
    console.log(response.data.files);
  };

  useEffect(() => {
    getFiles();
  }, []);

  const viewContext = useContext(SharedViewStateContext);

  useEffect(() => {
    viewContext.getContainerItems("dcanalyzerblob");
  }, []);

  const insert = () => {
    /*const newItem = {
          "Nombre del archivo": "NUEVO",
          "Tipo de archivo": "TXT",
          "Fecha de subida": new Date().toISOString(),
          "Progreso de subida": 0,
        };
        setData([...data, newItem]);*/
    // const id = setInterval(() => {
    //   setData((data) => {
    //     const newData = [...data];
    //     const index = data.findIndex(
    //       (el) => el["Nombre del archivo"] === "Minuta"
    //     );
    //     const newItem = { ...data[index] };
    //     if (newItem["Progreso de subida"] >= 100) {
    //       clearInterval(id);
    //       return data;
    //     }
    //     // @ts-ignore
    //     newItem["Progreso de subida"] += 10;
    //     newData[index] = newItem;
    //     return newData;
    //   });
    // }, 1000);
  };

  return (
    <Div>
      <Container>
        <Row>
          <h1>Archivos guardados</h1>
          <Button onClick={getFiles}>Refrescar</Button>
        </Row>
        <Table data={data} />

        {/* <div>
          <InputFile />
        </div>

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
        </div> */}
      </Container>
    </Div>
  );
};

export default HomeScreen;
