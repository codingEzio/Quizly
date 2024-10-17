<template>
  <div class="min-h-screen bg-gray-100">
    <nav class="bg-white shadow-md">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between h-16">
          <div class="flex">
            <div class="flex-shrink-0 flex items-center">
              <router-link to="/" class="flex items-center">
                <HomeIcon class="h-8 w-8 text-gray-800" />
                <h1 class="ml-2 text-xl font-bold text-gray-800">Quiz App</h1>
              </router-link>
            </div>
          </div>
          <div class="hidden sm:ml-6 sm:flex sm:space-x-8 items-center">
            <router-link to="/" class="nav-link flex items-center">
              <Squares2X2Icon class="h-6 w-6 mr-1" />
              Dashboard
            </router-link>
            <router-link to="/create" class="nav-link flex items-center">
              <PlusCircleIcon class="h-6 w-6 mr-1" />
              Create Quiz
            </router-link>
            <router-link to="/settings" class="nav-link flex items-center">
              <Cog6ToothIcon class="h-6 w-6 mr-1" />
              Settings
            </router-link>
          </div>
        </div>
      </div>
    </nav>

    <main class="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
      <router-view />
    </main>
  </div>
</template>

<script>
import { HomeIcon, Squares2X2Icon, PlusCircleIcon, Cog6ToothIcon } from '@heroicons/vue/24/outline'

export default {
  name: 'App',
  components: {
    HomeIcon,
    Squares2X2Icon,
    PlusCircleIcon,
    Cog6ToothIcon
  }
};
</script>

<style>
.nav-link {
  @apply px-3 py-2 rounded-md text-sm font-medium text-gray-700 hover:text-gray-900 hover:bg-gray-50;
}
.nav-link.router-link-exact-active {
  @apply text-gray-900 bg-gray-100;
}
</style>