<template>
    <el-dialog v-model="dialogVisible" :title="form.ID != '' ? '修改' : '新增'" width="30%" draggable
        @close="$emit('closeAdd')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="rules">
            <el-form-item label="名称" prop="Name">
                <el-input v-model="form.Name" />
            </el-form-item>
            <el-form-item label="路径" prop="Index">
                <el-input v-model="form.Index" />
            </el-form-item>
            <el-form-item label="图标" prop="Icon">
                <el-input v-model="form.Icon" />
            </el-form-item>
            <el-form-item label="组件名" prop="FilePath">
                <el-input v-model="form.FilePath" />
            </el-form-item>
            <el-form-item label="父级" prop="ParentId">
                <el-tree-select :props="{ value: 'Id', label: 'Name', children: 'Children' }" v-model="form.ParentId"
                    :data="store().UserMenus" check-strictly clearable />
            </el-form-item>
            <el-form-item label="排序" prop="Order">
                <el-input v-model.number="form.Order" />
            </el-form-item>
            <el-form-item label="是否启用" prop="IsEnable">
                <el-switch v-model="form.IsEnable" />
            </el-form-item>
            <el-form-item label="描述" prop="Description">
                <el-input v-model="form.Description" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="closeAdd(ruleFormRef)">Cancel</el-button>
                <el-button type="primary" @click="save(ruleFormRef)">Confirm</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script setup lang="ts">
import { ref, computed, defineEmits, reactive, watch } from "vue";
import { FormInstance, FormRules } from "element-plus";
import { AddMenu, EditMenu } from "../../../http/index";
import store from "../../../store/index";
import MenuModel from "../../../class/MenuModel";

const props = defineProps({
    isShow: Boolean,
    info: MenuModel
});
const dialogVisible = computed(() => props.isShow); // 绑定
const ruleFormRef = ref<FormInstance>();
const form = ref({
    ID: "",
    Name: "",
    Index: "",
    FilePath: "",
    ParentId: "",
    Order: 0,
    IsEnable: false,
    Icon: "",
    Description: "",
});
// 同表单关联
watch(() => props.info, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo;
    }
});
const rules = reactive<FormRules>({
    Name: [{ required: true, message: "请输入名称", trigger: "blur" }],
    Index: [{ required: true, message: "请输入路径", trigger: "blur" }],
    FilePath: [{ required: true, message: "请输入组件名称", trigger: "blur" }],
    Description: [{ required: true, message: "请输入描述", trigger: "blur" }],
    Order: [{ required: true, message: "请输入序号" }, { type: "number", message: "序号必须为数字" }],
});
const emits = defineEmits(["closeAdd", "success"]); // 定义事件列表
const closeAdd = async (formEl: FormInstance | undefined) => {
    if (!formEl) {
        return;
    }
    formEl.resetFields();
    emits("closeAdd"); // 父组件中的 closeAdd
};
const save = async (formEl: FormInstance | undefined) => {
    if (!formEl) {
        return;
    }
    await formEl.validate((valid, fields) => {
        if (valid) {
            if (form.value.ID) {
                EditMenu(form.value).then(function (res) {
                    if (res) {
                        emits("success", "修改成功！");
                    }
                });
            } else {
                AddMenu(form.value).then(function (res) {
                    if (res) {
                        emits("success", "添加成功！");
                    }
                });
            }
        } else {
            console.log("Error Submit!", fields);
        }
    });
};
</script>

<style scoped lang="scss"></style>