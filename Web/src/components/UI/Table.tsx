import { CLIENT_RENEG_WINDOW } from "node:tls";
import React from "react";
import styled from "styled-components";
import styled2 from "styled-components/cssprop";

const Div = styled.div`
  display: flex;
  flex-direction: column;
  box-sizing: border-box;
  border: 1px solid #ccc;
  border-radius: 8px;
  overflow: hidden;
  margin: 16px 0;
`;

const Row = styled.div`
  background-color: ${(p: any) => (p.accent ? "#eef0f2" : "unset")};
  gap: 8px;
  padding: 8px;
  display: flex;
`;

const Head = styled(Row)`
  border-bottom: 1px solid #ccc;
`;

const Cell = styled.div`
  flex: 1;
`;

const Table = (props: any) => {
  let items = null;
  let headers = null;
  if (props.data && props.data.length > 0) {
    items = props.data.map((el: any, i: number) => (
      <Row style={{ backgroundColor: i % 2 === 0 ? "#eef0f2" : "unset" }}>
        {Object.keys(el).map((key) => (
          <Cell>{el[key]}</Cell>
        ))}
      </Row>
    ));
    headers = (
      <Head>
        {Object.keys(props.data[0]).map((el) => (
          <Cell>
            <h3 style={{ margin: 0, color: "#1C448E" }}>{el}</h3>
          </Cell>
        ))}
      </Head>
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
