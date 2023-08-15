<template>
    <el-row>
        <el-col>
            <el-card>
                <el-form :model="form" lable-width="80px">
                    <el-form-item label="用户名">
                        <el-input v-model="form.NickName"></el-input>
                    </el-form-item>
                    <el-form-item label="密码">
                        <el-input v-model="form.Password" type="password"></el-input>
                    </el-form-item>
                    <el-form-item label="头像">
                        <el-upload class="avatar-uploader" :action="formAction" :show-file-list="false"
                            :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload">
                            <img v-if="faceImage" :src="faceImage" alt="用户头像" class="avatar">
                            <el-icon v-else class="avatar-uploadar-icon">
                                <Plus />
                            </el-icon>
                        </el-upload>
                    </el-form-item>
                    <el-form-item label="存储方式">
                        <el-radio-group v-model="form.UploadMode" @change="changeUploadMode">
                            <el-radio label="1" size="large">本地存储</el-radio>
                            <el-radio label="2" size="large">七牛云</el-radio>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="onSubmit">保存</el-button>
                        <el-button>取消</el-button>
                    </el-form-item>
                </el-form>
            </el-card>
            <el-card>
                <p style="color:red;">注意事项</p>
                <p>当前不支持短信验证相关功能，请谨慎修改密码</p>
                <p>单机部署可以选择本地存储，多服务器负载请上传到云</p>
                <p>目前内置七牛云，测试阶段使用</p>
                <p>......</p>
            </el-card>
        </el-col>
    </el-row>
</template>

<script setup lang="ts">
import { ref, reactive } from "vue";
import { ElMessage, UploadProps } from "element-plus";
import router from "../../router/index";
import userStore from "../../store/index";
import { FormatToken } from "../../tool/index";
import { EditNickNameOrPassword } from "../../http";
// --- 变量 ---
const form = reactive({
    NickName: FormatToken(userStore().token)?.NickName,
    Password: "",
    // FaceUrl: "/images/01.jpeg",
    Image: FormatToken(userStore().token)?.Image,
    UploadMode: "1"
});
const faceImage = ref(form.Image);
const formAction = ref("/api/File/UploadFaceImage?uploadMode=" + form.UploadMode);
// --- 方法 ---
/**
 * 变更上传目标
 */
const changeUploadMode = () => {
    formAction.value = "/api/File/UploadFaceImage?uploadMode=" + form.UploadMode;
};
/**
 * 上传前的校验
 * @param rawFile 
 */
const beforeAvatarUpload: UploadProps["beforeUpload"] = (rawFile) => {
    if (!(rawFile.type == "image/png" || rawFile.type == "image/jpeg")) {
        ElMessage.error("头像必需为图片格式！");
        return false;
    } else if ((rawFile.size / 1024 / 1024) > 2) {
        ElMessage.error("头像图片大小不能超过 2MB ！");
        return false;
    }
    return true;
};
/**
 * 上传
 * @param response 
 * @param uploadFile 
 */
const handleAvatarSuccess: UploadProps["onSuccess"] = (response, uploadFile) => {
    // 根据不同的上传方式，设置不同的访问路径
    if (form.UploadMode == "1") {
        // 本地
        form.Image = `http://localhost:5271/faceimages/${response.Result}`;
    } else {
        // 七牛云
        form.Image = `http://localhost:5271/faceimages/${response.Result}`;
    }
    faceImage.value = URL.createObjectURL(uploadFile.raw!);
};
/**
 * 保存
 */
const onSubmit = async () => {
    // 修改用户信息
    await EditNickNameOrPassword(form);
    // 退出 待实现...
    let count = 5;
    let myTime = setInterval(function () {
        if (count == 0) {
            userStore().$reset();
            router.push({ path: "/login" });
            // 清除倒计时
            clearInterval(myTime);
        } else {
            ElMessage.warning(`${count} 退出系统...`);
        }
        count--;
    }, 1000);
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
    transition: var(--el-transition-duration-fast);
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
