<template>
    <!-- 图标基础用法 -->
    <!-- <div>
        <el-icon>
            <ChatLineRound />
        </el-icon>
    </div> -->
    <!-- 图标按钮 -->
    <!-- <div>
        <el-button type="primary" icon="Search">Search</el-button>
    </div> -->
    <!-- 自定义图标 -->
    <!-- <div>
        <IconComp icon="Avatar"></IconComp>
    </div> -->
    <!-- 日历组件 -->
    <el-calendar v-model="currentDate">
        <template #date-cell="{ data }">
            <div style="width: 100%;height: 100%;" @click="SettingTodo(data.day)">
                <div>{{ data.day }}</div>
                <div>
                    {{ read(data.day) }}
                </div>
            </div>
        </template>
    </el-calendar>
    <el-dialog v-model="dialogFormVisible" title="待办事项" style="width: 500px;" draggable>
        <el-form :model="form" ref="formRef">
            <el-form-item label="事项" label-width="90px" prop="todo">
                <el-input v-model="form.todo" autocomplete="off"></el-input>
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="Cancel(formRef)">Cancel</el-button>
                <el-button @click="Submit(formRef)" type="primary">Confirm</el-button>
            </span>
        </template>
    </el-dialog>
</template>
<script setup lang="ts">
// import IconComp from "../../components/IconComp.vue";
import { ref } from "vue";
import type { FormInstance } from "element-plus"; // 导入类型

const currentDate = ref(new Date());
const todoList = ref([
    {
        day: "2023-07-19",
        todo: "待办事项一"
    },
    {
        day: "2023-07-20",
        todo: "待办事项二"
    },
    {
        day: "2023-07-21",
        todo: "待办事项三"
    }
]);
const read = (day: string) => {
    return todoList.value.find(x => x.day == day)?.todo;
};
const dialogFormVisible = ref(false);
const form = ref({
    day: "",
    todo: ""
});
const SettingTodo = (day: string) => {
    dialogFormVisible.value = true;
    form.value.day = day;
    // 查找当前日期是否存在
    let val = todoList.value.find(x => x.day == day)?.todo;
    form.value.todo = (val == undefined ? "" : val);
};
const formRef = ref<FormInstance>();
const Cancel = (formRef: FormInstance | undefined) => {
    dialogFormVisible.value = false;
    formRef?.resetFields();
};
const Submit = (formRef: FormInstance | undefined) => {
    dialogFormVisible.value = false;
    // 查询是否已存在
    let info = todoList.value.find(x => x.day == form.value.day);
    if (info == undefined) {
        // 无 - 新增
        todoList.value.push({
            day: form.value.day,
            todo: form.value.todo
        });
    } else {
        // 有 - 修改
        todoList.value.forEach(x => {
            if (x.day == form.value.day) {
                x.todo = form.value.todo;
            }
        });
    }
    // 清空表单
    formRef?.resetFields();
    // 验证表单数据 可参考 LoginPage.vue
};
</script>