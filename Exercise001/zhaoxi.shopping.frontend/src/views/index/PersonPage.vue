<template>
    <el-row>
        <el-col>
            <el-card>
                <el-form :model="form" label-width="80px">
                    <el-form-item label="用户名">
                        <el-input v-model="form.NickName" />
                    </el-form-item>
                    <el-form-item label="密码">
                        <el-input v-model="form.Password" />
                    </el-form-item>
                    <el-form-item label="头像">
                        <el-upload class="avatar-uploader" :action="formAction" :show-file-list="false"
                            :headers="{ 'Authorization': 'Bearer ' + store().Token }" :on-success="handleAvatarSuccess"
                            :before-upload="beforeAvatarUpload">
                            <img v-if="imageUrl" :src="imageUrl" class="avatar" />
                            <el-icon v-else class="avatar-uploader-icon">
                                <Plus />
                            </el-icon>
                        </el-upload>
                    </el-form-item>
                    <el-form-item label="存储方式">
                        <el-radio-group v-model="form.UploadMode" @change="changeMode">
                            <el-radio label="1" size="large">本地存储</el-radio>
                            <el-radio label="2" size="large">七牛云</el-radio>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item label="头像">
                        <el-button type="primary" @click="onSubmit">Create</el-button>
                        <el-button>Cancel</el-button>
                    </el-form-item>
                </el-form>
            </el-card>
            <el-card>
                <p style="color: red;">注意事项</p>
                <p>当前不支持短信验证功能，请谨慎修改密码</p>
                <p>单机部署可以选择本地存储，多服务器负载请上传到云</p>
                <p>目前内置七牛云，测试阶段使用</p>
                <p>......</p>
            </el-card>
        </el-col>
    </el-row>
</template>

<script setup lang="ts">
import { reactive, ref } from "vue";
import { ElMessage, UploadProps } from "element-plus";
import store from "../../store/index";
// import { FormatToken } from "../../tools/index";

const form = reactive({
    // NickName: FormatToken(store().Token)?.NickName,
    NickName: "",
    Password: "",
    // Image: FormatToken(store().Token)?.Image,
    Image: "",
    UploadMode: "1"
});
const imageUrl = ref(form.Image);
const formAction = ref("/api/File/UploadFile?mode=" + form.UploadMode);
const changeMode = () => {
    formAction.value = "/api/File/UploadFile?mode=" + form.UploadMode;
};
const handleAvatarSuccess: UploadProps["onSuccess"] = (response: any, uploadFile: any) => {
    // 根据上传方式设置不同的访问路径
    if (form.UploadMode == "1") {
        // 本地上传
        form.Image = `http://localhost:5148/static/${response.Result}`;
    } else {
        // 七牛云
        form.Image = `http://xxx.xxxx.xxx/${response.Result}`;
    }
    imageUrl.value = URL.createObjectURL(uploadFile.raw!);
};
// 上传前的校验
const beforeAvatarUpload: UploadProps["beforeUpload"] = (rawFile: any) => {
    if (!(rawFile.type == "image/png" || rawFile.type == "image/jpeg")) {
        ElMessage.error("Avatar picture must be JPG format!");
        return false;
    } else if ((rawFile.size / 1024 / 1024) > 2) {
        ElMessage.error("Avatar picture size can not exceed 2MB!");
        return false;
    }
    return true;
};
const onSubmit = async () => {
    console.log("onSubmit");
};
</script>

<style scoped lang="scss">
.avatar-uploader .avatar {
    width: 178px;
    height: 178px;
    display: block;
}

.avatar-uploader .el-upload {
    border: 1px dashed var(--el-border-color);
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: var(--el-transition-duration-fase);
}

.avatar-uploader .el-upload:hover {
    border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    text-align: center;
}

.el-card {
    float: left;
    width: 500px;
    margin-left: 20px;
}
</style>