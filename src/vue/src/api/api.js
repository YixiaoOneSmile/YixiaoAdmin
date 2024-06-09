import axios from 'axios';

let BaseUrl = 'http://localhost:5002/';

export const Url = BaseUrl;

export const Login = (params) => {
    return axios({
        method: 'Get',
        url: BaseUrl + 'Auth',
        params: params,
    })
        .then((res) => res)
        .catch((error) => console.log(error));
};
//以下代码由T4模板生成
export const SelectUser = (params) => {
    console.log(params);
    return axios({
        method: 'Post',
        url: BaseUrl + 'User/Pages',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const AddUser = (params) => {
    console.log(params);
    return axios({
        method: 'Post',
        url: BaseUrl + 'User',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const EditUser = (params) => {
    console.log(params);
    return axios({
        method: 'Put',
        url: BaseUrl + 'User',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const DeleteUser = (params) => {
    console.log(params);
    return axios({
        method: 'Delete',
        url: BaseUrl + 'User',
        params: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const SelectRole = (params) => {
    console.log(params);
    return axios({
        method: 'Post',
        url: BaseUrl + 'Role/Pages',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const SelectALLRole = () => {
    return axios({
        method: 'Get',
        url: BaseUrl + 'Role/All',
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const AddRole = (params) => {
    console.log(params);
    return axios({
        method: 'Post',
        url: BaseUrl + 'Role',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const EditRole = (params) => {
    console.log(params);
    return axios({
        method: 'Put',
        url: BaseUrl + 'Role',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const DeleteRole = (params) => {
    console.log(params);
    return axios({
        method: 'Delete',
        url: BaseUrl + 'Role',
        params: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const SelectRight = (params) => {
    console.log(params);
    return axios({
        method: 'Post',
        url: BaseUrl + 'Right/Pages',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};
export const SelectALLRight = () => {
    return axios({
        method: 'Get',
        url: BaseUrl + 'Right/All',
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};
export const AddRight = (params) => {
    console.log(params);
    return axios({
        method: 'Post',
        url: BaseUrl + 'Right',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const EditRight = (params) => {
    console.log(params);
    return axios({
        method: 'Put',
        url: BaseUrl + 'Right',
        data: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

export const DeleteRight = (params) => {
    console.log(params);
    return axios({
        method: 'Delete',
        url: BaseUrl + 'Right',
        params: params,
    })
        .then((res) => res.data)
        .catch((error) => console.log(error));
};

//T4模板生成结束
