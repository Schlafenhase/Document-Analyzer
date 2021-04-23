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
import { SharedViewStateContext } from "../azure-storage/contexts/viewStateContext";
import refreshIcon from "../assets/refresh-icon.svg";
import { Container } from "./UI/Container";
import Button from "./UI/Button";
import axios from "axios";
import { BaseURL } from "../constants";
import Dropzone from "./UI/Dropzone";
import EmployeeResultsTable from "./UI/EmployeeResultsTable";

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

  background: rgb(72, 135, 159);
  background: linear-gradient(
    90deg,
    rgba(72, 135, 159, 1) 0%,
    rgba(67, 193, 158, 1) 45%,
    rgba(67, 197, 158, 1) 55%,
    rgba(73, 122, 159, 1) 100%
  );
`;

const Row = styled.div`
  display: flex;
  justify-content: space-between;
`;

const Title = styled.h1`
  font-size: 24pt;
  margin: 0 10px 10px 10px;
  color: white;
`;

const InfoLabel = styled.p`
  color: white;
`;

const FileLabel = styled.div`
  color: white;
  font-size: 14pt;
  margin-top: 20px;
  margin-bottom: 20px;
  font-style: italic;
`;

const ProcessingLabel = styled.div`
  color: #c2c2c2;
  font-size: 14pt;
  margin-top: 20px;
  margin-bottom: 20px;
  font-weight: bold;
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
    setStatus("Processing File with NLP...");
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
    setStatus("Processing Complete");
    setTimeout(getFiles, 1000);
    setTimeout(setStatus, 2000);
  };

  const viewContext = useContext(SharedViewStateContext);

  useEffect(() => {
    viewContext.getContainerItems("dcanalyzerblob");
  }, []);

  if (!props.token) {
    window.location.reload();
    return null;
  }

  return (
    <Div>
      <Container>
        <Row>
          <Title>Saved Files</Title>
          <Button onClick={getFiles}>
            Refresh
            <img src={refreshIcon} />
          </Button>
        </Row>
        <Table onClickItem={getDetail} data={data} />
        <FileLabel>Upload new file:</FileLabel>

        {status ? (
          <ProcessingLabel>STATUS: {status}</ProcessingLabel>
        ) : (
          <div>
            <Dropzone
              start={() => setStatus("Uploading to Azure Blob Storage...")}
              uploaded={uploadFile}
            />
          </div>
        )}

        {fileData.title ? (
          [
            <FileLabel>Selected File: {fileData.title}</FileLabel>,
            <EmployeeResultsTable data={fileData.data} />,
          ]
        ) : (
          <FileLabel>Select a file to view details</FileLabel>
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
