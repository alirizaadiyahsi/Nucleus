<template>
  <div class="identity-form mt-3">
    <div class="">
      <router-view/>
      <div class="card p-fluid">
        <Dropdown v-model="selectedLanguage" :options="languages" optionLabel="name" :filter="true" placeholder="Select a Language" :showClear="true">
          <template #value="slotProps">
            <div class="" v-if="slotProps.value">
              <img :alt="slotProps.value.name" :src="require('@/assets/images/icons/flags/' + slotProps.value.code + '.png')"/>
              <div>{{ slotProps.value.name }}</div>
            </div>
            <span v-else>
          {{ slotProps.placeholder }}
      </span>
          </template>
          <template #option="slotProps">
            <div class="">
              <img :alt="slotProps.option.name" :src="require('@/assets/images/icons/flags/' + slotProps.option.code + '.png')"/>
              <div>{{ slotProps.option.name }}</div>
            </div>
          </template>
        </Dropdown>
      </div>
    </div>
  </div>


</template>

<script src="./identity-layout.ts"></script>
<style src="./identity-layout.scss" lang="scss"></style>