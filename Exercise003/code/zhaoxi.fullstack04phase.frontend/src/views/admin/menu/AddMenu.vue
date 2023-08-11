<template>
    <el-dialog v-model="dialogVisible" :title="form.Id != '' ? '编辑' : '新增'" width="30%">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="formRules">
            <el-form-item label="名称" prop="Name">
                <el-input v-model="form.Name"></el-input>
            </el-form-item>
            <el-form-item label="路径" prop="Index">
                <el-input v-model="form.Index"></el-input>
            </el-form-item>
            <el-form-item label="图标" prop="Icon">
                <el-input v-model="form.Icon"></el-input>
            </el-form-item>
            <el-form-item label="组件名称" prop="FilePath">
                <el-input v-model="form.FilePath"></el-input>
            </el-form-item>
            <el-form-item label="父级" prop="ParentId">
                <el-tree-select :props="{ value: 'Id', label: 'Name', children: 'Children' }" v-model="form.ParentId"
                    :data="userStore().UserMenus" check-strictly clearable></el-tree-select>
            </el-form-item>
            <el-form-item label="排序" prop="Order">
                <el-input v-model="form.Order"></el-input>
            </el-form-item>
            <el-form-item label="是否启用" prop="IsEnable">
                <el-switch v-model="form.IsEnable"></el-switch>
            </el-form-item>
            <el-form-item label="描述" prop="Description">
                <el-input v-model="form.Description"></el-input>
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="closeAddMenu(ruleFormRef)">取消</el-button>
                <el-button type="primary" @click="save(ruleFormRef)">提交</el-button>
            </span>
        </template>
    </el-dialog>
</template>
  
<script setup lang="ts">
import { ref, computed, reactive, watch } from "vue";
import { FormInstance, FormRules, ElMessage } from "element-plus";
import MenuModel from "../../../class/MenuModel";
import userStore from "../../../store/index";
import { AddMenu, EditMenu } from "../../../http/index";

// --- 变量 ---
const props = defineProps({
    isShow: Boolean,
    info: MenuModel
});
const dialogVisible = computed(() => props.isShow);
const ruleFormRef = ref<FormInstance>();
const form = ref({
    Id: "",
    Name: "",
    Index: "",
    FilePath: "",
    ParentId: "",
    Order: 0,
    IsEnable: false,
    Icon: "",
    Description: "",
});
const formRules = reactive<FormRules>({
    Name: [{ required: true, message: "请输入名称", trigger: "blur" }],
    Index: [{ required: true, message: "请输入路径", trigger: "blur" }],
    FilePath: [{ required: true, message: "请输入组件名称", trigger: "blur" }],
    Order: [{ required: true, message: "请输入序号" }, { type: "number", message: "该字段必须为数字" }]
});
const emits = defineEmits(["closeAddMenu", "success"]);

// --- 方法 ---
// 组件的实例只会在加载的时候渲染一次，若要实现 Form 的值和参数联动，需要使用监听
watch(() => props.info, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo;
    }
});
const closeAddMenu = async (formEl: FormInstance | undefined) => {
    if (!formEl) {
        return;
    }
    formEl.resetFields();
    emits("closeAddMenu");
};
const save = async (formEl: FormInstance | undefined) => {
    if (!formEl) {
        return;
    }
    await formEl.validate((valid, _fields) => {
        if (valid) {
            if (form.value.Id) {
                EditMenu(form.value).then(function (response) {
                    if (response) {
                        emits("success", "编辑成功！");
                    }
                });
            } else {
                AddMenu(form.value).then(function (response) {
                    if (response) {
                        emits("success", "新增成功！");
                    }
                });
            }
        } else {
            ElMessage.error("提交表单字段验证异常");
        }
    });
};
</script>
  
<style scoped lang="scss"></style>
  