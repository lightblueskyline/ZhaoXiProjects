<template>
    <el-dialog v-model="dialogVisible" title="设置菜单" style="width: 30vw;height: 50vw;" @close="$emit('closeSettingMenu')">
        <div>
            <el-tree-select ref="tree" style="width: 90%;" v-model="value" :data="userStore().UserMenus" :props="{
                children: 'Children',
                label: 'Name',
                value: 'Id'
            }" :defult-checked-keys="[14]" node-key="Id" :render-after-expand="false" show-checkbox
                mutiple></el-tree-select>
        </div>
        <template #footer>
            <span class="dialog-footer" style="position: absolute; bottom: 20px;left: 0px;">
                <el-button @click="closeSettingMenu()">取消</el-button>
                <el-button type="primary" @click="save()">提交</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";
import userStore from "../../../store";
import { RecursiveRoutes } from "../../../tool/index";
import { SettingMenu } from "../../../http";

// --- 变量 ---
const props = defineProps({
    isShowSettingMenu: Boolean,
    roleIds: String
});
const dialogVisible = computed(() => props.isShowSettingMenu);
const tree = ref();
const value = ref();
const emits = defineEmits(["closeSettingMenu", "successSettingMenu"]);

// --- 方法 ---
const closeSettingMenu = () => {
    emits("closeSettingMenu");
};
const save = async () => {
    // 获取当前选择的树节点，该函数会返回子节点和父节点
    // console.log(tree.value.getCheckedNodes());
    // 展开一棵树变为 List 结构
    // console.log(RecursiveRoutes(tree.value.getCheckedNodes()));
    // 通过 ES6 的 Map 方法，序列化数组中的每一项为 JSON 字符串，再通过 Set 集合排重，最后 Map 返回反序列化后的结果
    const uniqueArray = Array.from(new Set(RecursiveRoutes(tree.value.getCheckedNodes())
        .map(item => JSON.stringify(item))))
        .map(str => JSON.parse(str));
    // 通过 Filter 过滤掉父节点，返回被选中的子节点
    // console.log(uniqueArray.filter(x => x.Children == null).map(item => item.Id));
    // 组合参数
    let menuIds = uniqueArray.map(x => x.Id).join(",");
    // console.log(props.roleIds, menuIds);
    // Todo: 需要测试一下
    let result = (await SettingMenu(props.roleIds!, menuIds) as any) as boolean;
    if (result) {
        emits("successSettingMenu");
    }
};
</script>

<style scoped lang="scss"></style>