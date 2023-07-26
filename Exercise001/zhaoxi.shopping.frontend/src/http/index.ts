// 发起 HTTP 请求
import axios from "axios";
import { ElLoading, ElMessage } from "element-plus";
import ApiResult from "../class/ApiResult";
import store from "../store/index"; // 全局状态管理
import { FormatToken } from "../tools/index";
import router from "../router/index";

// 创建拦截器对象
const instance = axios.create({
    // timeout: 6000
    headers: {
        "Content-Type": "application/json",
        // "Authorization": "Bearer ***Token***"
    }
});

// 拦截请求
let loadingInstance: any; // 设置 Loading
instance.interceptors.request.use(config => {
    loadingInstance = ElLoading.service({
        lock: true,
        text: "Loading",
        background: "rgba(0,0,0,0.7)"
    });
    // 全局状态管理需要在这里获取，否则会提示没有初始化引用
    config.headers.Authorization = "Bearer " + store().Token;
    return config;
});

// 拦截响应
instance.interceptors.response.use(
    response => {
        loadingInstance.close();
        // 取得请求结果后统一返回，并设置返回结果
        const result: ApiResult = response.data;
        if (!result.IsSuccess) {
            ElMessage.error(result.Msg);
        }
        // return response;
        // 对于业务模块，只需关注结果，无需做验证提示
        return result.Result;
    },
    // 请求异常
    async error => {
        // 请求的配置对象
        const originalRequest = error.config;
        if (!originalRequest) {
            return Promise.reject(error);
        }
        // 1. 401 表示未授权
        // 2. _retry 表示是否应该重试
        // 3. RefreshTokenNum 表示重试次数
        // 4. 如果 pinia 里的全局状态等于空，则说明未登录，则不进行刷新逻辑
        if (error.response.status === 401 && !originalRequest._retry && store().RefreshTokenCount < 2 && store().Token != "") {
            console.log("进入重试机制...");
            // 每次重试时设置为 True
            originalRequest._retry = true;
            // 请求刷新 Token 的接口
            const newAccessToken = (await GetNewToken(FormatToken(store().Token)?.ID!)).data.Result as string;
            if (newAccessToken) {
                // 拿到 Token 之后更新全局状态，设置下次请求的 Token 返回实例
                let num = store().RefreshTokenCount + 1;
                store().$patch({
                    Token: newAccessToken,
                    RefreshTokenCount: num
                });
                instance.defaults.headers.common["Authorization"] = `Bearer ${newAccessToken}`;
                return instance(originalRequest);
            }
        } else if (error.response.status === 401 && router.currentRoute.value.path != "/") {
            console.log("进行路由跳转");
            router.push({
                path: "/login"
            });
        } else if (error.response.status === 500) {
            ElMessage.error("内部服务器错误，请检查服务是否启动！");
        } else if (error.response.status === 404) {
            ElMessage.error("接口地址不存在，请检查接口地址！");
        }
        loadingInstance.close();
        // 请求失败则返回错误信息
        return error;
    }
);

// 获取 Token
export const GetToken = (obj: {}) => {
    return instance.post("/api/Login/GetToken", obj);
};
// 刷新 Token
export const GetNewToken = (userID: string) => {
    // 未使用拦截器
    return axios.get(`/api/Login/RefreshToken?userID=${userID}`);
};

// 菜单模块
export const AddMenu = (req: {}) => {
    return instance.post("/api/Menu/Add", req);
};
export const EditMenu = (req: {}) => {
    return instance.post("/api/Menu/Edit", req);
};
export const DeleteMenu = (id: string) => {
    return instance.get(`/api/Menu/Delete?id=${id}`);
};
export const GetMenus = (req: {}) => {
    return instance.post("/api/Menu/GetMenus", req);
};

// 角色模块
export const AddRole = (req: {}) => {
    return instance.post("/api/Role/Add", req);
};
export const EditRole = (req: {}) => {
    return instance.post("/api/Role/Edit", req);
};
export const DeleteRole = (id: string) => {
    return instance.get(`/api/Role/Delete?id=${id}`);
};
export const GetRoles = (req: {}) => {
    return instance.post("/api/Role/GetRoles", req);
};
export const SettingMenu = (roleID: string, menuIDs: string) => {
    return instance.get(`/api/Menu/Delete?roleID=${roleID}&menuIDs=${menuIDs}`);
};

// 用户模块
export const GetUsers = (obj: {}) => {
    return instance.post("/api/User/GetUsers", obj)
}
export const AddUser = (req: {}) => {
    return instance.post("/api/User/Add", req)
}
export const EditUser = (req: {}) => {
    return instance.post("/api/User/Edit", req)
}
export const DeleteUser = (id: string) => {
    return instance.get(`/api/User/Delete?id=${id}`)
}
export const SettingRole = (uid: string, rids: string) => {
    return instance.get(`/api/User/SettingRole?uid=${uid}&rids=${rids}`)
}

// 个人信息模块
export const EditPersonInfo = (req: {}) => {
    return instance.post(`/api/User/EditNickNameOrPassword`, req)
}