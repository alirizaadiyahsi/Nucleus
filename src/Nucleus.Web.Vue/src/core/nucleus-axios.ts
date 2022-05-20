import axios from "axios";
import {AppConsts} from "@/core/consts/app-consts";

const NucleusAxios = axios.create({
    baseURL: `${AppConsts.apiUrl}/api/`,
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json"
    }
});

NucleusAxios.interceptors.response.use(
    response => response,
    error => {
        if (error.response.status === 401 || error.response.status === 403) {
            window.location.href = '/identity/login';
        }
        return Promise.reject(error);
    }
);

export default NucleusAxios;