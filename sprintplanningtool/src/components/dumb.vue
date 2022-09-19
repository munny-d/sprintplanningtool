<script:ts>
export default defineComponent({
  name: 'TypescriptExamples',
  props: {
    name: {
      type: String,
      required: true
    },
    id: [Number, String],
    success: { type: String },
    callback: {
      type: Function as PropType<() => void>
    },
    book: {
      type: Object as PropType<Book>,
      required: true
    },
    metadata: {
      type: null // metadata is typed as any
    },
    bookA: {
      type: Object as PropType<Book>,
      // Make sure to use arrow functions
      default: () => ({
        title: 'Arrow Function Expression'
      }),
      validator: (book: Book) => !!book.title
    },
    bookB: {
      type: Object as PropType<Book>,
      // Or provide an explicit this parameter
      default(this: void) {
        return {
          title: 'Function Expression'
        }
      },
      validator(this: void, book: Book) {
        return !!book.title
      }
    }
  },
  setup(props) {
    const result = props.name.split('') // correct, 'name' is typed as a string
    const year = ref(2020)
    const yearsplit = year.value.split('')// => Property 'split' does not exist on type 'number'
    const stringNumberYear = ref<string | number>('2020') // year's type: Ref<string | number>
 
    stringNumberYear.value = 2020 // ok!
    const modal = ref<InstanceType<typeof MyModal>>()
    const openModal = () => {
      modal.value?.open()
    }
    const book = reactive<Book>({ title: 'Vue 3 Guide' })
    // or
    const book1: Book = reactive({ title: 'Vue 3 Guide' })
    // or
    const book2 = reactive({ title: 'Vue 3 Guide' }) as Book
 
    const handleChange = (evt: Event) => {
      console.log((evt.target as HTMLInputElement).value)
    }
    return {
      modal, openModal,
      book, book1, book2,
      handleChange
    };
},
  emits: {
    addBook(payload: { bookName: string }) {
      // perform runtime validation
      return payload.bookName.length > 0
    }
  },
  methods: {
    onSubmit() {
      this.$emit('addBook', {
        bookName: '123'
     //   bookName: 123 // Type error!
      })
 
    //  this.$emit('non-declared-event') // Type error!
    }
  }

</script:ts>
