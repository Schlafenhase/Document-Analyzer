import React, {useContext, useEffect, useState} from "react";
import styled from "styled-components";
import Button from "./UI/Button";
import Table from "./UI/Table";
import ContainerList from "../azure-storage/components/ContainerList";
import SelectedContainer from "../azure-storage/components/SelectedContainer";
import InputFile from "../azure-storage/components/InputFile";
import ItemsList from "../azure-storage/components/ItemsList";
import ItemsUploaded from "../azure-storage/components/ItemsUploaded";
import ItemsDownloaded from "../azure-storage/components/ItemsDownloaded";
import ItemsDeleted from "../azure-storage/components/ItemsDeleted";
import {SharedViewStateContext, UploadsViewStateContext} from "../azure-storage/contexts/viewStateContext";
import {tap} from "rxjs/operators";
import {ContainerItem} from "@azure/storage-blob";

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
    const viewContext = useContext(SharedViewStateContext);

    useEffect(() => {
        viewContext.getContainerItems("dcanalyzerblob")
        },
        [])

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
                // @ts-ignore
                newItem["Progreso de subida"] += 10;
                newData[index] = newItem;
                return newData;
            });
        }, 1000);
    };

    return (
        <Div>
            <Table data={data}/>
            <div>
                <InputFile/>
            </div>

            <hr/><hr/>
            <h2>Debug Components - Delete Them Later:</h2>
            <ContainerList/>
            <hr/>
            <SelectedContainer className="container"/>
            <ItemsList/>

            <div className="item-details">
                <ItemsUploaded/>
                <ItemsDownloaded/>
                <ItemsDeleted/>
            </div>
        </Div>
    );
};

export default HomeScreen;
