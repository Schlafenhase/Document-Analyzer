import axios from "axios";

//export const BaseURL = "https://daapi.conveyor.cloud";
//export const BaseURL = "https://localhost:44328";
export const BaseURL = "http://d288a604bb24.ngrok.io";

export const checkToken =  async () => {
    const savedToken = localStorage.getItem("token");
    if (savedToken) {
      const response = await axios.get(BaseURL + "/Api/Employee/Employees", {
        headers: {
          Authorization: "Bearer " + savedToken,
        },
      });
      if (response.data) {
        console.log();
        return savedToken
      }
    }
    return ""
  };
