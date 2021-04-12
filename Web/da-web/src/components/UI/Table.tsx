import React from "react";
import styled from "styled-components";

const Div = styled.div`
  display: flex;
  flex-direction: column;
  box-sizing: border-box;
  border: 1px solid black;
  border-radius: 8px;
  margin: 16px 0;
`;

const Row = styled.div`
  padding: 8px 0;
  display: flex;
`;

const Cell = styled.div`
  text-align: center;
  flex: 1;
`;

const Table = (props: any) => {
    let items = null;
    let headers = null;
    if (props.data) {
        items = props.data.map((el: any) => (
            <Row>
                {Object.keys(el).map((key) => (
                    <Cell>{el[key]}</Cell>
                ))}
            </Row>
        ));
        headers = (
            <Row>
                {Object.keys(props.data[0]).map((el) => (
                    <Cell>
                        <h3 style={{ margin: 0, color: "#1C448E" }}>{el}</h3>
                    </Cell>
                ))}
            </Row>
        );
    }

    return (
        <Div>
            {headers}
            {items}
        </Div>
    );
};

export default Table;
