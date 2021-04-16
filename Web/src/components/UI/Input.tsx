import styled from "styled-components";

const Container = styled.div`
  position: relative;
  display: flex;
  flex-direction: column;
`;

const Input = (props: any) => {
  return (
    <Container>
      <input
        {...props}
        placeholder={props.label}
        value={props.state[0]}
        onChange={(e) => props.state[1](e.target.value)}
      />
    </Container>
  );
};

export default Input;
